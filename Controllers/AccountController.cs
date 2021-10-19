using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.ViewModels;

namespace ShopApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Registration(UserRegisterViewModel model)
        {
            return View();
        }
    }
}
