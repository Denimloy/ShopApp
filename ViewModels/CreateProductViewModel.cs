using ShopApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class CreateProductViewModel
    {
        public Product Product { get; set; }
        public int[] selectedProductAttributeId { get; set; }
        public List<AttributesTemplate> AttributesTemplates { get; set; }
    }
}
