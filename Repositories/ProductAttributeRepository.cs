using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Data;
using Microsoft.Extensions.Logging;

namespace ShopApp.Repositories
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger _logger;
        public ProductAttributeRepository(AppDbContext appDbContext, ILogger<ProductAttribute> logger)
        {
            _db = appDbContext;
            _logger = logger;
        }
        public async Task<bool> CreateAsync(ProductAttribute productAttribute)
        {
            try
            {
                _db.ProductAttributes.Add(productAttribute);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAsync method error");

                return false;
            }
        }
    }
}
