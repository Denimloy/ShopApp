using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllMainCategoriesAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<bool> CreateAsync(Category category);
        Task<bool> EditAsync(Category category);
        Task<bool> DeleteAsync(int categoryId);
        Task<IEnumerable<Category>> GetAllSubcategoriesAsync();
    }
}
