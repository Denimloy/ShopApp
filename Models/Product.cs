using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public int? Price { get; set; }

        [Required]
        [StringLength(125, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<Image> Images { get; set; }
        public Product()
        {
            ProductAttributes = new List<ProductAttribute>();
            Images = new List<Image>();
        }

    }
}
