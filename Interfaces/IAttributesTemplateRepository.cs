using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IAttributesTemplateRepository
    {
        Task<bool> CreateAsync(AttributesTemplate attributesTemplate);
        Task<List<AttributesTemplate>> GetAllAttributesTemplatesAsync();
        Task<AttributesTemplate> GetAttributesTemplateByIdAsync(int id);
        Task<bool> EditAsync(AttributesTemplate attributesTemplate);
        Task<bool> DeleteAsync(int Id);
        Task<List<AttributesTemplate>> GetAttributesTemplatesByCategoryIdAsync(int categoryId);
    }
}
