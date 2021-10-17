using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ShopApp.ViewModels;

namespace ShopApp.ValidationAttributes
{
    public class MaxNumberOfUploadedFilesAttribute : ValidationAttribute
    {
        public MaxNumberOfUploadedFilesAttribute()
        {
            ErrorMessage = "You can only upload a maximum of 6 images";
        }
        public override bool IsValid(object value)
        {
            IFormFileCollection files = value as IFormFileCollection;
            if ((files != null) && (files.Count > 6))
                return false;

            return true;
        }
    }
}
