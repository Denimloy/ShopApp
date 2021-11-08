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
        private readonly IProductAttributeRepository _productAttributes;
        public ProductController(ICategoryRepository category, IProductRepository products, IAttributesTemplateRepository attributesTemplates, IProductAttributeRepository productAttributes)
        {
            this._productAttributes = productAttributes;
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
                model.Product.ProductAttributes.AddRange(await _productAttributes.GetRangeByIdAsync(model.selectedProductAttributeId));

                bool result = await _products.CreateAsync(model.Product);
                if(result)
                {
                    return RedirectToAction("Index", "Home");
                }

                return StatusCode(500);
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
