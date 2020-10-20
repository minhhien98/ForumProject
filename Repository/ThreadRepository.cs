using DomainModel.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ThreadRepository : IThreadRepository
    {
        private DatabaseContext _dbContext;
        public ThreadRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateThread(Thread thread)
        {
            _dbContext.Threads.Add(thread);
            _dbContext.SaveChanges();
        }

        public void DeleteThread(Thread thread)
        {
            _dbContext.Threads.Remove(thread);
            _dbContext.SaveChanges();
        }

        public void EditThread(Thread thread)
        {
            _dbContext.Entry(thread).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public Thread GetThreadById(int id)
        {
            return _dbContext.Threads.Include(t => t.Section).FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Thread> ThreadList()
        {
            return _dbContext.Threads.Include(t => t.Section).ToList();
        }
    }
}
