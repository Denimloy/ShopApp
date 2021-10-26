using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface ICategoriesTitleRepository
    {
        Task<List<CategoriesTitle>> GetAllCategoriesTitlesAsync();
        Task<bool> CreateAsync(CategoriesTitle categoriesTitle);
    }
}
