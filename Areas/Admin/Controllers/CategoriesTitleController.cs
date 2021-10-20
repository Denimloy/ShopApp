using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;
using ShopApp.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesTitleController : Controller
    {
        private readonly ICategoriesTitleRepository _categoriesTitles;
        public CategoriesTitleController(ICategoriesTitleRepository categoriesTitle)
        {
            this._categoriesTitles = categoriesTitle;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriesTitle categoriesTitle)
        {
            if(ModelState.IsValid)
            {
                await _categoriesTitles.CreateAsync(categoriesTitle);
                return RedirectToAction("CreateMainCategory", "Category");
            }
            return View(categoriesTitle);
        }
    }
}
