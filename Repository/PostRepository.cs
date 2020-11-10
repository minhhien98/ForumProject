using DomainModel.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class PostRepository : IPostRepository
    {
        private DatabaseContext _dbContext;
        public PostRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreatePost(Post post)
        {
            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
        }

        public void DeletePost(Post post)
        {
            var item = _dbContext.Posts.Include(p => p.Comments).First(p => p.Id == post.Id);
            _dbContext.Posts.Remove(item);
            _dbContext.SaveChanges();
        }

        public void EditPost(Post post)
        {
            _dbContext.Entry(post).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Post GetPostById(int id)
        {
            return _dbContext.Posts.Include(p => p.Thread).Include(p=>p.User).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> PostList()
        {
            return _dbContext.Posts.Include(p => p.Thread).Include(p=>p.User).Include(p=>p.Comments).ToList();
        }
    }
}
