using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopApp.ViewModels;

namespace ShopApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryRepository _categories;
        private readonly IProductRepository _products;
        private readonly IAttributesTemplateRepository _attributesTemplates;
        public ProductController(ICategoryRepository category, IProductRepository products, IAttributesTemplateRepository attributesTemplates)
        {
            this._attributesTemplates = attributesTemplates;
            this._products = products;
            this._categories = category;
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _categories.GetAllSubcategoriesAsync();
            ViewBag.Сategories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [Route("admin/Product/_GetProductAttributesPartial/{categoryId}")]
        public async Task<IActionResult> _GetProductAttributesPartial(int categoryId)
        {
            var attributesTemplates = await _attributesTemplates.GetAttributesTemplatesByCategoryIdAsync(categoryId);

            CreateProductViewModel model = new CreateProductViewModel() { AttributesTemplates = attributesTemplates };

            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            for(int i = 0; i < model.selectedProductAttributeId?.Length; i++)
            {
                if(model.selectedProductAttributeId[i] == 0)
                {
                    ModelState.AddModelError($"selectedProductAttributeId[{i}]", "The field is required.");
                }
            }
            if(ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            if(model.Product.CategoryId != null)
            {
                model.AttributesTemplates = await _attributesTemplates.GetAttributesTemplatesByCategoryIdAsync((int)model.Product.CategoryId);
            }

            var categories = await _categories.GetAllSubcategoriesAsync();
            ViewBag.Сategories = new SelectList(categories, "Id", "Name");

            return View(model);
        }
    }
}
