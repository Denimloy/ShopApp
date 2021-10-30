using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class AttributesTemplate
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z &]+$", ErrorMessage = "Only non-numeric characters.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public AttributesTemplate()
        {
            ProductAttributes = new List<ProductAttribute>();
        }

    }
}
