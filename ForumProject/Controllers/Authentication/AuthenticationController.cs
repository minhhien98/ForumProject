using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumProject.Models.ViewModel;
using ForumProject.Models.ViewModel.Authentication;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace ForumProject.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        private IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            var result = _authenticationService.Login(vm.Username, vm.Password, vm.RememberMe);
            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(vm.Password, "Invalid Username/Password");
            return View(vm);
        }
        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult Logout()
        {
            _authenticationService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}