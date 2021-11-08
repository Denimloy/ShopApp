using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var productAttribute = await _db.ProductAttributes.FirstOrDefaultAsync(x => x.Id == id);

                _db.ProductAttributes.Remove(productAttribute);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync method error");

                return false;
            }
        }

        public async Task<bool> EditAsync(ProductAttribute productAttribute)
        {
            try
            {
                _db.ProductAttributes.Update(productAttribute);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "EditAsync method error");

                return false;
            }
        }

        public async Task<IEnumerable<ProductAttribute>> GetAllProductAttributesAsync()
        {
            try
            {
                return await Task.Run(() => _db.ProductAttributes.AsNoTracking());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllProductAttributesAsync method error");

                List<ProductAttribute> productAttributes = new List<ProductAttribute>();

                return productAttributes;
            }
        }

        public Task<ProductAttribute> GetProductAttributeByIdAsync(int id)
        {
            try
            {
                return _db.ProductAttributes.Include(x => x.AttributesTemplate).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetProductAttributeByIdAsync method error");

                return null;
            }
        }

        public async Task<IEnumerable<ProductAttribute>> GetRangeByIdAsync(int[] identifiers)
        {
            try
            {
                return await Task.Run(() => _db.ProductAttributes.AsNoTracking().Where(x => identifiers.Contains(x.Id)));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetRangeByIdAsync method error");

                List<ProductAttribute> productAttributes = new List<ProductAttribute>();

                return productAttributes;
            }
        }
    }
}
