using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Service.Interface;
using X.PagedList;

namespace ForumProject.Controllers
{
    public class PostController : Controller
    {
        private IMapper _mapper;
        private IPostService _postService;
        private IUserService _userService;
        private IThreadService _threadService;
        public PostController(IMapper mapper,IPostService postService,IUserService userService,IThreadService threadService)
        {
            _mapper = mapper;
            _postService = postService;
            _userService = userService;
            _threadService = threadService;
        }
        public IActionResult Index(int id,int? page)
        {
            var pageNumber = page ?? 1;
            var list = _postService.PostList().Where(p => p.ThreadId == id).ToPagedList(pageNumber, 10);
            ViewBag.ThreadId = id;
            ViewBag.Thread = _threadService.GetThreadById(id).ThreadName;
            return View(list);
        }
        [Authorize]
        public IActionResult Create(int id)
        {
            var vm = new PostViewModel()
            {
                ThreadId = id,
                UserId = _userService.GetUserByUserName(User.Identity.Name).Id,
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(PostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var post = _mapper.Map<Post>(vm);
                post.PostDate = DateTimeOffset.UtcNow;
                post.Id = 0;
                post.IsClosed = false;
                _postService.CreatePost(post);
                return RedirectToAction("UserPostList", "Post");
                //return RedirectToAction("Index", new { id = vm.ThreadId });
            }
            return View(vm);
        }
        [Authorize]
        public IActionResult UserPostList()
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var list = _postService.PostList().Where(p => p.UserId == user.Id);
            return View(list);
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var post = _postService.GetPostById(id);
            if(user.Id == post.UserId || User.IsInRole("Admin"))
            {
                var vm = _mapper.Map<PostViewModel>(post);
                return View(vm);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Edit(PostViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var post = _mapper.Map<Post>(vm);
                _postService.EditPost(post);
                return RedirectToAction("UserPostList","Post");
            }
            return View(vm);
        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var post = _postService.GetPostById(id);
            if (user.Id == post.UserId || User.IsInRole("Admin"))
            {
                var vm = _mapper.Map<PostViewModel>(post);
                return View(vm);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Delete(PostViewModel vm)
        {
            var user = _userService.GetUserByUserName(User.Identity.Name);
            var post = _postService.GetPostById(vm.Id);
            if (user.Id == post.UserId || User.IsInRole("Admin"))
            {
                _postService.DeletePost(post);
                return RedirectToAction("UserPostList","Post");
            }
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public IActionResult CloseTopic(int PostId)
        {
            var post = _postService.GetPostById(PostId);
            post.IsClosed = true;
            _postService.EditPost(post);
            return RedirectToAction("Index", "Comment", new { id = post.Id });
        }
    }
}
