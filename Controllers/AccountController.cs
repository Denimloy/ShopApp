using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Controllers
{
    public class AccountController : Controller
    {
        public async Task<IActionResult> Registering()
        {
            return View();
        }
    }
}
