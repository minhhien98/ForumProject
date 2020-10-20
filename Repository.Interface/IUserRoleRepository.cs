using DomainModel.Entity;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Repository.Interface
{
    public interface IUserRoleRepository
    {
        void CreateRole(UserRole userRole);
        void EditRole(UserRole userRole);
        void DeleteRole(UserRole userRole);
        UserRole GetUserRoleById(int id);
        IEnumerable<UserRole> RoleList();

    }
}
