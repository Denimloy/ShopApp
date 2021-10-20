using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.ViewModels;
using ShopApp.Interfaces;
using ShopApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ShopApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _users;
        public AccountController(IUserRepository userRepository)
        {
            this._users = userRepository;
        }
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool emailUsed = await _users.CheckEmailForAvailabilityAsync(model.Email);
                if(emailUsed)
                {
                    ModelState.AddModelError("Email", "Email address already in use");
                }
                else
                {
                    await _users.CreateAsync(new User { Name = model.Name, Email = model.Email.ToLower(), Password = model.Password });

                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool correctData = await _users.CheckLoginDetailsAsync(model.Email, model.Password);
                if(correctData)
                {
                    await Authenticate(model.Email);

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Wrong email or password. Please try again.");
            }
            return View(model);
        }
        private async Task Authenticate(string userEmail)
        {
            var user = await _users.GetUserAsync(userEmail);

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


    }
}
