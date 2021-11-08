using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Models;

namespace ShopApp.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> CreateAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
    }
}
