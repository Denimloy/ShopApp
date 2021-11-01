using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Value { get; set; }
        [Required]
        [Display(Name = "Attributes Template")]
        public int? AttributesTemplateId { get; set; }
        public AttributesTemplate AttributesTemplate { get; set; }
        public ICollection<Product> Products { get; set; }

        public ProductAttribute()
        {
            Products = new List<Product>();
        }
    }
}
