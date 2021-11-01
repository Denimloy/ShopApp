using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;
using ShopApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ShopApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductAttributeController : Controller
    {
        private readonly IProductAttributeRepository _productAttributes;
        private readonly IAttributesTemplateRepository _attributesTemplates;
        private readonly ICategoryRepository _categories;
        public ProductAttributeController(IProductAttributeRepository productAttributeRepository, IAttributesTemplateRepository attributesTemplateRepository,ICategoryRepository categoryRepository)
        {
            _categories = categoryRepository;
            _attributesTemplates = attributesTemplateRepository;
            _productAttributes = productAttributeRepository;
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categories.GetAllSubcategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }
        [HttpGet]
        [Route("admin/ProductAttribute/_GetAttributesTemplatesPartial/{categoryId}")]
        public async Task<IActionResult> _GetAttributesTemplatesPartial(int categoryId)
        {
            var attributesTemplates = await _attributesTemplates.GetAttributesTemplatesByCategoryIdAsync(categoryId);
            ViewBag.AttributesTemplates = new SelectList(attributesTemplates, "Id", "Name");

            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductAttributeViewModel model)
        {
            if(ModelState.IsValid)
            {
                bool result = await _productAttributes.CreateAsync(model.ProductAttribute);
                
                if(result)
                {
                    return RedirectToAction("Index", "Home");
                }

                return StatusCode(500);
            }
            if(model.CategoryId != null)
            {
                var attributesTemplates = await _attributesTemplates.GetAttributesTemplatesByCategoryIdAsync((int)model.CategoryId);
                ViewBag.AttributesTemplates = new SelectList(attributesTemplates, "Id", "Name");
            }

            var categories = await _categories.GetAllSubcategoriesAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(model);

        }
    }
}
