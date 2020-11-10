using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Utility;
using X.PagedList;

namespace ForumProject.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        private IPostService _postService;
        private IUserService _userService;
        public CommentController(ICommentService commentService,IPostService postService,IUserService userService)
        {
            _commentService = commentService;
            _postService = postService;
            _userService = userService;
        }
        public IActionResult Index(int id,int? page)
        {           
            var pageNumber = page ?? 1;
            var CurrentUser = _userService.GetUserByUserName(User.Identity.Name);
            var post = _postService.GetPostById(id);
            var comments = _commentService.CommentList().Where(c => c.PostId == id).ToPagedList(pageNumber,10);      
            var vm = new CommentViewModel()
            {
                CurrentUser = CurrentUser,
                Post = post,
                comments = comments,
            };
            ViewBag.PageNumber = pageNumber;
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(int PostId,string StrComment)
        {
            if (StrComment != null)
            {
                var user = _userService.GetUserByUserName(User.Identity.Name);
                var comment = new Comment()
                {
                    UserId = user.Id,
                    PostId = PostId,
                    Content = StrComment,
                    CommentDate = DateTimeOffset.UtcNow,
                };
                _commentService.CreateComment(comment);
                return RedirectToAction("Index", new { id = PostId });
            }
            return RedirectToAction("Index", new { id = PostId });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(int CommentId,string CommentContent)
        {
            var Comment = _commentService.GetCommentById(CommentId);
            if (CommentContent != null)
            {
                Comment.Content = CommentContent;
                _commentService.EditComment(Comment);
                return RedirectToAction("Index", "Comment", new { id = Comment.PostId });
            }
            return View("Index", new { id = Comment.PostId});
        }   
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int CommentId)
        {
            var comment = _commentService.GetCommentById(CommentId);
            if (comment != null)
            {
                _commentService.DeleteComment(comment);
                return RedirectToAction("Index", "Comment", new { id = comment.PostId });
            }
            return NotFound();
        }
    }
}
