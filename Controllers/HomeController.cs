using Microsoft.AspNetCore.Mvc;
using ShopApp.Interfaces;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryRepository _categories;

        public HomeController(ICategoryRepository categoryRepository)
        {
            _categories = categoryRepository;
        }
        public IActionResult Index()
        {
            //var categories = await _categories.GetAllCategoriesAsync();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

