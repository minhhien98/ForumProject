using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Models.ViewModel.Comment;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace ForumProject.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService _commentService;
        private IPostService _postService;
        public CommentController(ICommentService commentService,IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }
        public IActionResult Index(int id)
        {
            var post = _postService.GetPostById(id);
            var comments = _commentService.CommentList().Where(c => c.PostId == id).ToList();
            var vm = new CommentViewModel()
            {
                Post = post,
                comments = comments,
            };
            return View(vm);
        }
    }
}
