using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.ValidationAttributes
{
    public class ImageFileFormatAttribute : ValidationAttribute
    {
        public ImageFileFormatAttribute()
        {
            ErrorMessage = "Please select image format JPEG(.jpeg, .jpg) or PNG(.png).";
        }
        public override bool IsValid(object value)
        {
            if(value != null)
            {
                if(value is IFormFileCollection)
                {
                    IFormFileCollection files = value as IFormFileCollection;
                    foreach (var file in files)
                    {
                        if ((file.ContentType != "image/jpeg") && (file.ContentType != "image/png"))
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    IFormFile file = value as IFormFile;
                    if ((file.ContentType != "image/jpeg") && (file.ContentType != "image/png"))
                    {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
