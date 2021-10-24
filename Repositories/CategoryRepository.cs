using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;
using ShopApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace ShopApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly FileSystemRepository _fileSystem;
        public CategoryRepository(AppDbContext appDbContext, FileSystemRepository fileSystem)
        {
            this._fileSystem = fileSystem;
            this._db = appDbContext;
        }

        public async Task CreateAsync(Category category)
        {
            category.Name = await Task.Run(() => PrepareCategoryNameForSaving(category.Name));

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int categoryId)
        {
            SqlParameter paramCategoryId = new SqlParameter
            {
                ParameterName = "@categoryId",
                Value = categoryId
            };

            var paramImagePath = new SqlParameter
            {
                ParameterName = "@imagePath",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Direction = System.Data.ParameterDirection.Output,
                Size = -1
            };
            await _db.Database.ExecuteSqlRawAsync("DeleteCategoryWithImage @categoryId, @imagePath OUT", paramCategoryId, paramImagePath);
            string imagePath = paramImagePath.Value.ToString();
            _fileSystem.DeleteImageFromFileSystemAsync(imagePath);
        }

        public async Task EditAsync(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _db.Categories.AsNoTracking().ToListAsync();
;
        }

        public async Task<List<Category>> GetAllMainCategoriesAsync()
        {
            
            return await _db.Categories
                .Where(x => x.Parent == null)
                .Include(x => x.Children)
                .ThenInclude(x => x.Children)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _db.Categories
                .Include(x => x.CategoriesTitle)
                .Include(x => x.Image)
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        private static string PrepareCategoryNameForSaving(string categoryName)
        {
            //Create an array of strings by spaces and remove extra spaces
            string[] stringArray = categoryName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string correctedCategoryName = "";
            char space = ' ';
            //Capitalize each word
            foreach (var item in stringArray)
            {
                if (item.Length > 1)
                {
                    correctedCategoryName += char.ToUpper(item[0]) + item.Substring(1) + " ";
                }
                else
                {
                    correctedCategoryName += item.ToUpper() + " ";
                }
            }
            return correctedCategoryName.TrimEnd(space);
        }

    }
}
