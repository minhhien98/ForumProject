using DomainModel.Entity;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserRoleService : IUserRoleService
    {
        private IUserRoleRepository _userRoleRepository; 
        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
        public void CreateRole(UserRole userRole)
        {
            _userRoleRepository.CreateRole(userRole);
        }

        public void DeleteRole(UserRole userRole)
        {
            _userRoleRepository.DeleteRole(userRole);
        }

        public void EditRole(UserRole userRole)
        {
            _userRoleRepository.EditRole(userRole);
        }

        public UserRole GetUserRoleById(int id)
        {
            return _userRoleRepository.GetUserRoleById(id);
        }

        public IEnumerable<UserRole> RoleList()
        {
            return _userRoleRepository.RoleList();
        }
    }
}
