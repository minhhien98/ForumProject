using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ICommentService
    {
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
        Comment GetCommentById(int id);
        IEnumerable<Comment> CommentList();
    }
}
