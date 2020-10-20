using DomainModel.Entity;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private DatabaseContext _dbContext;
        public UserRoleRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateRole(UserRole userRole)
        {
            _dbContext.UserRoles.Add(userRole);
            _dbContext.SaveChanges();
        }

        public void DeleteRole(UserRole userRole)
        {
            _dbContext.UserRoles.Remove(userRole);
            _dbContext.SaveChanges();
        }

        public void EditRole(UserRole userRole)
        {
            _dbContext.Entry(userRole).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public UserRole GetUserRoleById(int id)
        {
            return _dbContext.UserRoles.Find(id);
        }

        public IEnumerable<UserRole> RoleList()
        {
            return _dbContext.UserRoles.ToList();
        }
    }
}
