using DomainModel.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IUserRoleService
    {
        void CreateRole(UserRole userRole);
        void EditRole(UserRole userRole);
        void DeleteRole(UserRole userRole);
        UserRole GetUserRoleById(int id);
        IEnumerable<UserRole> RoleList();
    }
}
