using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using ShopApp.Models;
using Microsoft.AspNetCore.Http;
using ShopApp.ViewModels;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ImageController : Controller
    {
        private readonly IImageRepository _images;

        public ImageController(IImageRepository imageRepository)
        {
            this._images = imageRepository;
        }
        [HttpGet]
        public IActionResult CreateCategoryImage()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategoryImage(CreateCategoryImageViewModel model)
        {
            int categoryId = (int)HttpContext.Session.GetInt32("newCategoryId");

            if(ModelState.IsValid)
            {
                await _images.CreateCategoryImageAsync(model.UploadedImage, categoryId);
                HttpContext.Session.Remove("newCategoryId");
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult EditMainCategoryImage(int categoryId)
        {
            HttpContext.Session.SetInt32("categoryId", categoryId);
            ViewBag.CategoryId = categoryId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainCategoryImage(CreateCategoryImageViewModel model)
        {
            int categoryId = (int)HttpContext.Session.GetInt32("categoryId");

            if (ModelState.IsValid)
            {
                await _images.EditCategoryImageAsync(model.UploadedImage, categoryId);
                HttpContext.Session.Remove("categoryId");
                return RedirectToAction("EditCategory", "Category", new { categoryId = categoryId });
            }

            ViewBag.CategoryId = categoryId;
            return View();
        }
    }
}
