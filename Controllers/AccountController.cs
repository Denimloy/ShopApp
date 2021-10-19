using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.ViewModels;
using ShopApp.Interfaces;
using ShopApp.Models;

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
                bool emailUsed = await _users.CheckAddressForAvailabilityAsync(model.Email);
                if(emailUsed)
                {
                    ModelState.AddModelError("Email", "Email address already in use");
                }
                else
                {
                    await _users.CreateAsync(new User { Name = model.Name, Email = model.Email, Password = model.Password });
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }
        

    }
}
