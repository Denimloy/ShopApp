using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllMainCategoriesAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<bool> CreateAsync(Category category);
        Task<bool> EditAsync(Category category);
        Task<bool> DeleteAsync(int categoryId);
    }
}
