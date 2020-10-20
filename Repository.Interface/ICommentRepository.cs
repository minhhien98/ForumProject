using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Repository.Interface
{
    public interface ICommentRepository
    {
        void CreateComment(Comment comment);
        void EditComment(Comment comment);
        void DeleteComment(Comment comment);
        Comment GetCommentById(int id);
        IEnumerable<Comment> CommentList();
    }
}
