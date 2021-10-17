using Microsoft.AspNetCore.Http;
using ShopApp.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.ViewModels
{
    public class CreateCategoryImageViewModel
    {
        [Required(ErrorMessage = "The field is required.")]
        [ImageFileFormat]
        public IFormFile UploadedImage { get; set; }
    }

}
