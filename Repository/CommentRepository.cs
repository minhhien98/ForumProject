using DomainModel.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CommentRepository : ICommentRepository
    {
        private DatabaseContext _dbContext;
        public CommentRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateComment(Comment comment)
        {
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Comment> CommentList()
        {
            return _dbContext.Comments.Include(c => c.User).Include(c => c.Post).ToList();
        }

        public void DeleteComment(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }

        public void EditComment(Comment comment)
        {
            _dbContext.Entry(comment).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Comment GetCommentById(int id)
        {
            return _dbContext.Comments.Include(c=>c.User).Include(c=>c.Post).FirstOrDefault(c=>c.Id == id);
        }
    }
}
