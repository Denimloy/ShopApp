using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using ShopApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categories;
        private readonly ICategoriesTitleRepository _categoriesTitles;
        private readonly IImageRepository _images;
        public CategoryController(ICategoryRepository categoryRepository, ICategoriesTitleRepository categoriesTitleRepository, IImageRepository imageRepository)
        {
            this._images = imageRepository;
            this._categoriesTitles = categoriesTitleRepository;
            this._categories = categoryRepository;
        }
        public async Task<IActionResult> CreateMainCategory()
        {
            var categoriesTitles = await _categoriesTitles.GetAllCategoriesTitlesAsync();
            ViewBag.CategoriesTitles = new SelectList(categoriesTitles, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMainCategory(Category category)
        {
            if(category.CategoriesTitleId == null)
            {
                ModelState.AddModelError("CategoriesTitleId", "The Category Title field is required.");
            }
            if(ModelState.IsValid)
            {
                await _categories.CreateAsync(category);
                HttpContext.Session.SetInt32("newCategoryId", category.Id);

                return RedirectToAction("CreateCategoryImage", "Image");
            }

            var categoriesTitles = await _categoriesTitles.GetAllCategoriesTitlesAsync();
            ViewBag.CategoriesTitles = new SelectList(categoriesTitles, "Id", "Name");

            return View(category);
        }

        public async Task<IActionResult> CreateSubcategory()
        {

            var categories = await _categories.GetAllMainCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubcategory(Category category)
        {
            if(category.ParentId == null)
            {
                ModelState.AddModelError("ParentId", "The Main Category field is required.");
            }
            if(ModelState.IsValid)
            {
                await _categories.CreateAsync(category);
                return RedirectToAction("Index", "Home");
            }

            var categories = await _categories.GetAllMainCategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(category);
        }

        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categories.GetAllCategoriesAsync();
            return View(categories);
        }
        public async Task<IActionResult> EditMainCategory(int categoryId)
        {
            Category category = await _categories.GetCategoryByIdAsync(categoryId);

            var categoriesTitles = await _categoriesTitles.GetAllCategoriesTitlesAsync();
            ViewBag.CategoriesTitles = new SelectList(categoriesTitles, "Id", "Name");

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categories.EditAsync(category);
                return RedirectToAction("GetAllCategories");
            }

            var categoriesTitles = await _categoriesTitles.GetAllCategoriesTitlesAsync();
            ViewBag.CategoriesTitles = new SelectList(categoriesTitles, "Id", "Name");

            return View(category);
            
        }

        public async Task<IActionResult> EditSubcategory(int subcategoryId)
        {
            Category subcategory = await _categories.GetCategoryByIdAsync(subcategoryId);

            var mainCategories = await _categories.GetAllMainCategoriesAsync();
            ViewBag.MainCategories = new SelectList(mainCategories, "Id", "Name");

            return View(subcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubcategory(Category subcategory)
        {
            if (ModelState.IsValid)
            {
                await _categories.EditAsync(subcategory);
                return RedirectToAction("GetAllCategories");
            }

            var mainCategories = await _categories.GetAllMainCategoriesAsync();
            ViewBag.MainCategories = new SelectList(mainCategories, "Id", "Name");

            return View(subcategory);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            Category category = await _categories.GetCategoryByIdAsync(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            await _categories.DeleteAsync(id);

            return RedirectToAction("GetAllCategories");
        }



    }
}
