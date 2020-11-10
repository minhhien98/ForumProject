using DomainModel.Entity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public void CreateComment(Comment comment)
        {
            _commentRepository.CreateComment(comment);
        }

        public IEnumerable<Comment> CommentList()
        {
            return _commentRepository.CommentList();
        }

        public void DeleteComment(Comment comment)
        {
            _commentRepository.DeleteComment(comment);
        }

        public void EditComment(Comment comment)
        {
            _commentRepository.EditComment(comment);
        }

        public Comment GetCommentById(int id)
        {
            return _commentRepository.GetCommentById(id);
        }
    }
}
