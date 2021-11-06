using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using System.Diagnostics;
using ShopApp.ViewModels;
using System;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using ShopApp.Services;
using System.Threading.Tasks;

namespace ShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ErrorService _error; 
        public HomeController(ErrorService errorServis)
        {
            this._error = errorServis;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("/Error")]
        public async Task<IActionResult> Error(int code)
        {

            ErrorViewModel model = await _error.HandleErrorAsync(code);

            if (model.StatusCode >= 500)
            {
                return View("ServerError", model);
            }

            return View("ClientError", model);
        }
    }
}

