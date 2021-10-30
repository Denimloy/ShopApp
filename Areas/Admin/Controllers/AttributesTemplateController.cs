using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;
using ShopApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ShopApp.ViewModels;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AttributesTemplateController : Controller
    {
        private readonly ICategoryRepository _categories;
        private readonly IAttributesTemplateRepository _attributesTemplates;

        public AttributesTemplateController(ICategoryRepository categoryRepository, IAttributesTemplateRepository attributesTemplateRepository)
        {
            this._attributesTemplates = attributesTemplateRepository;
            this._categories = categoryRepository;
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categories.GetAllSubcategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttributesTemplate attributesTemplate)
        {
            if(ModelState.IsValid)
            {
                bool result = await _attributesTemplates.CreateAsync(attributesTemplate);

                if(result)
                {
                    return RedirectToAction("Index", "Home");
                }

                return StatusCode(500);
            }

            return View(attributesTemplate);
        }
        public async Task<IActionResult> GetAllAttributesTemplates()
        {
            var attributesTemplates = await _attributesTemplates.GetAllAttributesTemplatesAsync();

            return View(attributesTemplates);

        }
        public async Task<IActionResult> Edit(int id)
        {
            var attributesTemplate = await _attributesTemplates.GetAttributesTemplateByIdAsync(id);

            var categories = await _categories.GetAllSubcategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(attributesTemplate);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AttributesTemplate attributesTemplate)
        {
            if(ModelState.IsValid)
            {
                bool result = await _attributesTemplates.EditAsync(attributesTemplate);

                if(result)
                {
                    return RedirectToAction("GetAllAttributesTemplates");
                }

                return StatusCode(500);
            }

            return View(attributesTemplate);
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var attributesTemplate = await _attributesTemplates.GetAttributesTemplateByIdAsync(id);

            return View(attributesTemplate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            bool result = await _attributesTemplates.DeleteAsync(id);
            if (result)
            {
                return RedirectToAction("GetAllAttributesTemplates");
            }
            else
            {
                return StatusCode(500);
            }

        }


    }
}
