using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using ShopApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using ShopApp.Data;
using ShopApp.Services;

namespace ShopApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly FileSystemRepository _fileSystem;
        private readonly ILogger _logger;
        private readonly TextEditorService _textEditor;

        public CategoryRepository(AppDbContext appDbContext, FileSystemRepository fileSystem, ILogger<CategoryRepository> logger, TextEditorService textEditor)
        {
            this._textEditor = textEditor;
            this._logger = logger;
            this._fileSystem = fileSystem;
            this._db = appDbContext;
        }

        public async Task<bool> CreateAsync(Category category)
        {
            try
            {
                category.Name = await Task.Run(() => _textEditor.CapitalizeEachWord(category.Name).ToString());

                _db.Categories.Add(category);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {

                _logger.LogError(ex, "CreateAsync method error");

                return false;

            }

        }

        public async Task<bool> DeleteAsync(int categoryId)
        {
            try
            {
                SqlParameter paramCategoryId = new()
                {
                    ParameterName = "@categoryId",
                    Value = categoryId
                };

                SqlParameter paramImagePath = new()
                {
                    ParameterName = "@imagePath",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Direction = System.Data.ParameterDirection.Output,
                    Size = -1
                };

                await _db.Database.ExecuteSqlRawAsync("DeleteCategoryWithImage @categoryId, @imagePath OUT", paramCategoryId, paramImagePath);
                string imagePath = paramImagePath.Value.ToString();
                _fileSystem.DeleteImageFromFileSystemAsync(imagePath);

                return true;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync method error");

                return false;
            }

        }

        public async Task<bool> EditAsync(Category category)
        {
            try
            {
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "EditAsync method error");

                return false;
            }
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _db.Categories.AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllCategoriesAsync method error");

                List<Category> categories = new List<Category>();

                return categories;
            }
;
        }

        public async Task<List<Category>> GetAllMainCategoriesAsync()
        {
            try
            {
                return await _db.Categories
                    .Where(x => x.Parent == null)
                    .Include(x => x.Children)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllMainCategoriesAsync method error");

                List<Category> categories = new List<Category>();

                return categories;
            }


        }
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                return await _db.Categories
                    .Include(x => x.Image)
                    .FirstOrDefaultAsync(x => x.Id == categoryId);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllMainCategoriesAsync method error");

                return null;
            }
        }

        public async Task<List<Category>> GetAllSubcategoriesAsync()
        {
            try
            {
                return await _db.Categories.Where(x => x.ParentId != null).AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllSubcategoriesAsync method error");

                List<Category> categories = new List<Category>();
                return categories;
            }
        }
    }
}
