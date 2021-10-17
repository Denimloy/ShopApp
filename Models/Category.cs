using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z &]+$", ErrorMessage = "Only non-numeric characters.")]
        public string Name { get; set; }
        [Display(Name = "Main Category")]
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Category> Children { get; set; }
        [Display(Name = "Category Title")]
        public int? CategoriesTitleId { get; set; }
        public CategoriesTitle CategoriesTitle { get; set; }
        public Image Image { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<AttributesTemplate> AttributesTemplates { get; set; }
        public Category()
        {
            Children = new List<Category>();
            Products = new List<Product>();
            AttributesTemplates = new List<AttributesTemplate>();
        }
    }
}
