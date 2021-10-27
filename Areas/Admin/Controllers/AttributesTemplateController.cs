using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;
using ShopApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
                AttributesTemplate template = new AttributesTemplate { Name = attributesTemplate.Name, CategoryId = attributesTemplate.CategoryId };
                bool result = await _attributesTemplates.CreateAsync(template);
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

    }
}
