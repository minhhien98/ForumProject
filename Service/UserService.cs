using DomainModel.Entity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Service
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void CreateUser(User user)
        {
            _userRepository.CreateUser(user);
        }

        public void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user);
        }

        public void EditUser(User user)
        {
            _userRepository.EditUser(user);
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByUserName(string username)
        {
            return _userRepository.GetUserByUserName(username);
        }

        public IEnumerable<User> UserList()
        {
            return _userRepository.UserList();
        }
    }
}
