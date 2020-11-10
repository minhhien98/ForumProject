using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private IAuthenticationService _authenticationService;
        public UserController(IMapper mapper,IUserService userService,IUserRoleService userRoleService,IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _userService = userService;
            _userRoleService = userRoleService;
            _authenticationService = authenticationService;
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
                //_authenticationService.Login(user.UserName, user.Password, false);
                TempData["StatusMessage"] = "Register successfully, Please Login!";
                return RedirectToAction("Login", "Authentication");
            }
            return View(vm);
        }
        [Authorize]
        public IActionResult ChangeAvatar()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult ChangeAvatar(ChangeAvatarViewModel vm)
        {
            if(ModelState.IsValid)
            {
                var user = _userService.GetUserByUserName(User.Identity.Name);
                //Image img = Image.FromFile(Path.GetFullPath(vm.AvatarImage.FileName));
                user.AvatarImage = Utility.ImageConverter.ImageToByteArray(vm.AvatarImage);
                _userService.EditUser(user);
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("AvatarImage", "Please Insert Image!");
            return View();
        }
        
        //Admin Only
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
                vm.AvatarImage = Utility.ImageConverter.ImageToByteArray("wwwroot/Images/Avatar/default.png");
                var user = _mapper.Map<User>(vm);
                //user.Password = PasswordHelper.Sha256(user.Password, user.UserName);
                _userService.CreateUser(user);
                return RedirectToAction("UserList", "User");
            }
            vm.UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList();
            return View(vm);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            //var vm = _mapper.Map<UserViewModel>(user);
            var vm = new UserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AvatarImage = user.AvatarImage,
                UserRoleId = user.UserRoleId,
                UserRole = _userRoleService.RoleList().Select(r => new SelectListItem() { Value = r.Id.ToString(), Text = r.RoleName }).ToList(),               
            };
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
            var vm = new UserViewModel() { 
            Id= user.Id,
            UserName = user.UserName,
            Password = user.Password,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName= user.LastName,
            UserRoleId = user.UserRoleId,
            AvatarImage = user.AvatarImage
            };
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