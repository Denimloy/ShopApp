using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class CreateAttributesTemplateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
    }
}
