using DomainModel.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext _dbContext;
        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public void EditUser(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.Include(u=>u.UserRole).FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByUserName(string username)
        {
            return _dbContext.Users.Include(u => u.UserRole).FirstOrDefault(u => u.UserName == username);
        }

        public IEnumerable<User> UserList()
        {
            return _dbContext.Users.Include(u => u.UserRole).ToList();
        }
    }
}
