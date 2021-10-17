using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Repositories
{
    public class CategoriesTitleRepository : ICategoriesTitleRepository
    {
        private readonly AppDbContext _db;

        public CategoriesTitleRepository(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }
        public async Task CreateAsync(CategoriesTitle categoriesTitle)
        {
            categoriesTitle.Name = await Task.Run(() => PrepareTitleNameForSaving(categoriesTitle.Name));

            _db.CategoriesTitles.Add(categoriesTitle);
            await _db.SaveChangesAsync();
        }

        public async Task<List<CategoriesTitle>> GetAllCategoriesTitlesAsync()
        {
            return await _db.CategoriesTitles.AsNoTracking().ToListAsync();
        }
        private string PrepareTitleNameForSaving(string titleName)
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
