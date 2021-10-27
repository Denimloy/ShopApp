using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Areas.Admin.Controllers
{
    public class ProductAttributeController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductAttribute productAttribute)
        {
            return View();
        }
    }
}
