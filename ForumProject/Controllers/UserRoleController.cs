using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Entity;
using ForumProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Microsoft.AspNetCore.Authorization;
using ForumProject.Models.ViewModel.User;

namespace ForumProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserRoleController : Controller
    {
        private IUserRoleService _userRoleService;
        private IMapper _mapper;
        public UserRoleController(IUserRoleService userRoleService,IMapper mapper)
        {
            _userRoleService = userRoleService;
            _mapper = mapper;
        }       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<UserRole>(vm);
                _userRoleService.CreateRole(role);
                return RedirectToAction("List", "UserRole");
            }
            return View(vm);
        }
        public IActionResult List()
        {
            var list = _userRoleService.RoleList();
            return View(list);
        }
        public IActionResult Edit(int id)
        {
            var role = _userRoleService.GetUserRoleById(id);
            var vm = _mapper.Map<UserRoleViewModel>(role);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(UserRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<UserRole>(vm);
                _userRoleService.EditRole(role);
                return RedirectToAction("List", "UserRole");
            }
            return View(vm);
        }
        public IActionResult Delete(int id)
        {
            var role = _userRoleService.GetUserRoleById(id);
            var vm = _mapper.Map<UserRoleViewModel>(role);
            return View(vm);
        }
        [HttpPost]
        public IActionResult Delete(UserRoleViewModel vm)
        {
            var role = _userRoleService.GetUserRoleById(vm.Id);
            _userRoleService.DeleteRole(role);
            return RedirectToAction("List", "UserRole");
        }
    }
}