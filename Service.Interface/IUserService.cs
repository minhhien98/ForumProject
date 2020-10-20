using DomainModel.Entity;
using System;
using System.Collections.Generic;

namespace Service.Interface
{
    public interface IUserService
    {
        void CreateUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
        User GetUserById(int id);
        User GetUserByUserName(string username);
        IEnumerable<User> UserList();
    }
}
