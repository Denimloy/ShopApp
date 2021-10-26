using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IImageRepository
    {
        Task<bool> CreateCategoryImageAsync(IFormFile uploadedImage, int categoryId);
        Task<bool> EditCategoryImageAsync(IFormFile uploadedImage, int categoryId);
        Task<bool> DeleteCategoryImageAsync(int categoryId);
    }
}
