using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Models
{
    public class CategoriesTitle
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-z &]+$", ErrorMessage = "Only non-numeric characters.")]
        public string Name { get; set; }
        public ICollection<Category> Categories { get; set; }
        public CategoriesTitle()
        {
            Categories = new List<Category>();
        }
    }
}
