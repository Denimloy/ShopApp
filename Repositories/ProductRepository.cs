using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Data;

namespace ShopApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _db;
        public ProductRepository(ILogger<ProductRepository> logger, AppDbContext appDbContext)
        {
            this._db = appDbContext;
            this._logger = logger;
        }
        public async Task<bool> CreateAsync(Product product)
        {
            try
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAsync method error");

                return false;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            try
            {
                return await Task.Run(() => _db.Products);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllAsync method error");

                List<Product> products = new List<Product>();

                return products;
            }
        }
    }
}
