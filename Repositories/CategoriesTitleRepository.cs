using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Repositories
{
    public class CategoriesTitleRepository : ICategoriesTitleRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger _logger;

        public CategoriesTitleRepository(AppDbContext appDbContext, ILogger<CategoriesTitleRepository> logger)
        {
            this._logger = logger;
            this._db = appDbContext;
        }
        public async Task<bool> CreateAsync(CategoriesTitle categoriesTitle)
        {
            try
            {
                categoriesTitle.Name = await Task.Run(() => PrepareTitleNameForSaving(categoriesTitle.Name));

                _db.CategoriesTitles.Add(categoriesTitle);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateAsync method error");

                return false;
            }
        }

        public async Task<List<CategoriesTitle>> GetAllCategoriesTitlesAsync()
        {
            try
            {
                return await _db.CategoriesTitles.AsNoTracking().ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "GetAllCategoriesTitlesAsync method error");

                List<CategoriesTitle> categoriesTitles = new List<CategoriesTitle>();

                return categoriesTitles;
            }
        }
        private static string PrepareTitleNameForSaving(string titleName)
        {
            //Create an array of strings by spaces and remove extra spaces
            string[] stringArray = titleName.ToLower().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string correctedTitleName = "";
            char space = ' ';
            //Capitalize each word
            foreach (var item in stringArray)
            {
                if (item.Length > 1)
                {
                    correctedTitleName += char.ToUpper(item[0]) + item.Substring(1) + " ";
                }
                else
                {
                    correctedTitleName += item.ToUpper() + " ";
                }
            }
            return correctedTitleName.TrimEnd(space);
        }
    }
}
