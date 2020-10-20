using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel;
using ForumProject.Models.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Interface;
using Utility;

namespace ForumProject.Controllers
{
    public class UserController : Controller
    {
        private IMapper _mapper;
        private IUserService _userService;
        private IUserRoleService _userRoleService;
        public UserController(IMapper mapper,IUserService userService,IUserRoleService userRoleService)
        {
            _mapper = mapper;
            _userService = userService;
            _userRoleService = userRoleService;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            var UserExisted = _userService.GetUserByUserName(vm.UserName);
            if(UserExisted != null)
            {
                ModelState.AddModelError("UserName", "User name already existed!");
                return View(vm);
            }
            if (ModelState.IsValid)
            {               
                var user = _mapper.Map<User>(vm);
                _userService.CreateUser(user);
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult List()
        {
            var list = _userService.UserList();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            UserViewModel vm = new UserViewModel()
            {
                UserRole = _userRoleService.RoleList().Select(r=> new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName}).ToList(),
            };
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Create(UserViewModel vm)
        {
            var userExisted = _userService.GetUserByUserName(vm.UserName);
            if(userExisted != null)
            {
                ModelState.AddModelError("UserName", "User name already existed!");
                vm.UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList();
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(vm);
                //user.Password = PasswordHelper.Sha256(user.Password, user.UserName);
                _userService.CreateUser(user);
                return RedirectToAction("List", "User");
            }
            vm.UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList();
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            var vm = _mapper.Map<UserViewModel>(user);
            vm.UserRoleId = user.UserRoleId;
            vm.UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList();
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Edit(UserViewModel vm)
        {
            var userExisted = _userService.GetUserByUserName(vm.UserName);
            if (userExisted != null)
            {
                ModelState.AddModelError("UserName", "User name already existed!");
                vm.UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList();
                return View(vm);
            }
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(vm);
                _userService.EditUser(user);
                return RedirectToAction("List", "User");
            }
            vm.UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList();
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            var vm = _mapper.Map<UserViewModel>(user);
            return View(vm);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public IActionResult Delete(UserViewModel vm)
        {
            var user = _userService.GetUserById(vm.Id);
            _userService.DeleteUser(user);
            return RedirectToAction("List", "User");
        }
    }
}