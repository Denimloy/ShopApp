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
                bool result = await _categoriesTitles.CreateAsync(categoriesTitle);
                if(result)
                {
                    return RedirectToAction("CreateMainCategory", "Category");
                }
                else
                {
                    return StatusCode(500);
                }
            }
            return View(categoriesTitle);
        }
    }
}
