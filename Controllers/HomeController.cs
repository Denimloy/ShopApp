using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using System.Diagnostics;
using ShopApp.ViewModels;
using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace ShopApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Error")]
        public IActionResult Error(int code)
        {
            ErrorViewModel model = new ErrorViewModel { StatusCode = code, ErrorMessage = "Something went wrong." };
            if (code >= 500)
            {
                return View("ServerError", model);
            }
            if (code == 404)
            {
                model.ErrorMessage = "Page not found.";
            }

            return View("ClientError", model);
        }
    }
}

