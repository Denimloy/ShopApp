using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.ViewModels
{
    public class CreateProductAttributeViewModel
    {
        [Required]
        [Display( Name = "Category")]
        public int? CategoryId { get; set; }
        public List<AttributesTemplate> AttributesTemplates { get; set; }
        public ProductAttribute ProductAttribute { get; set; }
    }
}
