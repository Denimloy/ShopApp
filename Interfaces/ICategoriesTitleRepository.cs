using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface ICategoriesTitleRepository
    {
        Task<IEnumerable<CategoriesTitle>> GetAllCategoriesTitlesAsync();
        Task<bool> CreateAsync(CategoriesTitle categoriesTitle);
    }
}
