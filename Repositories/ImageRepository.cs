using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopApp.Interfaces;
using ShopApp.Models;

namespace ShopApp.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly AppDbContext _db;
        private readonly FileSystemRepository _imageService;
        public ImageRepository(AppDbContext appDbContext, FileSystemRepository imageService)
        {
            this._imageService = imageService;
            this._db = appDbContext;
        }

        public async Task CreateProductImageCollectionAsync(IFormFileCollection uploadedImages, int productId)
        {
            foreach (var image in uploadedImages)
            {
                await CreateProductImageAsync(image, productId);
            }
        }
        
        public async Task CreateProductImageAsync(IFormFile uploadedImage, int productId)
        {
            string path = await _imageService.SaveProductImageInFileSystemAsync(uploadedImage);

            Image image = new() { ProductId = productId, Path = path };
            _db.Images.Add(image);
            await _db.SaveChangesAsync();
        }

        public async Task CreateCategoryImageAsync(IFormFile uploadedImage, int categoryId)
        {

            string path = await _imageService.SaveCategoryImageInFileSystemAsync(uploadedImage);

            Image image = new() { CategoryId = categoryId, Path = path };
            _db.Images.Add(image);
            await _db.SaveChangesAsync();

        }

        public async Task EditCategoryImageAsync(IFormFile uploadedImage, int categoryId)
        {
            Image image = await _db.Images.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            _imageService.DeleteImageFromFileSystemAsync(image.Path);

            string path = await _imageService.SaveCategoryImageInFileSystemAsync(uploadedImage);
            image.Path = path;
            await EditAsync(image);
        }
        private async Task EditAsync(Image image)
        {
            _db.Images.Update(image);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteCategoryImageAsync(int categoryId)
        {
            Image image = await _db.Images.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
            if(image != null)
            {
                _imageService.DeleteImageFromFileSystemAsync(image.Path);
                _db.Images.Remove(image);
                await _db.SaveChangesAsync();
            }
        }
    }
}
