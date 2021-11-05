using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Data;
using System.Text;
using ShopApp.Services;

namespace ShopApp.Repositories
{
    public class CategoriesTitleRepository : ICategoriesTitleRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger _logger;
        private readonly TextEditor _textEditor;

        public CategoriesTitleRepository(AppDbContext appDbContext, ILogger<CategoriesTitleRepository> logger, TextEditor textEditor)
        {
            this._textEditor = textEditor;
            this._logger = logger;
            this._db = appDbContext;
        }
        public async Task<bool> CreateAsync(CategoriesTitle categoriesTitle)
        {
            try
            {
                categoriesTitle.Name = await Task.Run(() => _textEditor.CapitalizeEachWord(categoriesTitle.Name).ToString());

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
    }
}
