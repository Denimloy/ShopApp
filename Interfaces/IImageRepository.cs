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
        Task CreateProductImageCollectionAsync(IFormFileCollection uploadedImages, int productId);
        Task CreateCategoryImageAsync(IFormFile uploadedImage, int categoryId);
        Task EditCategoryImageAsync(IFormFile uploadedImage, int categoryId);
        Task DeleteCategoryImageAsync(int categoryId);
    }
}
