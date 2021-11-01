using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopApp.Interfaces;
using ShopApp.Models;
using ShopApp.Data;

namespace ShopApp.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _db;
        private readonly FileSystemRepository _imageService;
        private readonly ILogger _logger;
        public ImageRepository(AppDbContext appDbContext, FileSystemRepository imageService, ILogger<ImageRepository> logger)
        {
            this._logger = logger;
            this._imageService = imageService;
            this._db = appDbContext;
        }

        public async Task<bool> CreateCategoryImageAsync(IFormFile uploadedImage, int categoryId)
        {
            try
            {
                string path = await _imageService.SaveCategoryImageInFileSystemAsync(uploadedImage);

                Image image = new() { CategoryId = categoryId, Path = path };
                _db.Images.Add(image);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "CreateCategoryImageAsync method error");

                return false;
            }

        }

        public async Task<bool> EditCategoryImageAsync(IFormFile uploadedImage, int categoryId)
        {
            try
            {
                Image image = await _db.Images.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
                if(image == null)
                {
                    _logger.LogInformation("EditCategoryImageAsync method value: {categoryId}", categoryId);

                    return false;
                }

                _imageService.DeleteImageFromFileSystemAsync(image.Path);

                string path = await _imageService.SaveCategoryImageInFileSystemAsync(uploadedImage);
                image.Path = path;

                bool result = await EditAsync(image);
                if(!result)
                {
                    return false;
                }

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "EditCategoryImageAsync method error");

                return false;
            }
        }
        private async Task<bool> EditAsync(Image image)
        {
            try
            {
                _db.Images.Update(image);
                await _db.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "EditAsync method error");

                return false;
            }
        }

        public async Task<bool> DeleteCategoryImageAsync(int categoryId)
        {
            try
            {
                Image image = await _db.Images.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
                if (image == null)
                {
                    _logger.LogInformation("DeleteCategoryImageAsync method value: {categoryId}", categoryId);

                    return false;
                }

                _imageService.DeleteImageFromFileSystemAsync(image.Path);
                _db.Images.Remove(image);
                await _db.SaveChangesAsync();
                
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "DeleteCategoryImageAsync method error");

                return false;
            }
        }
    }
}
