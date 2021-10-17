using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopApp.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;

namespace ShopApp.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _appEnvironment;
        
        public ImageService(IWebHostEnvironment appEnvironment)
        {
            this._appEnvironment = appEnvironment;
        }

        public async Task<string> SaveProductImageInFileSystemAsync(IFormFile uploadedImage)
        {
            string directoryName = "products";

            return await SaveImageAsync(uploadedImage, directoryName);
        }

        public async Task<string> SaveCategoryImageInFileSystemAsync(IFormFile uploadedImage)
        {
            string directoryName = "categories";

            return await SaveImageAsync(uploadedImage, directoryName);
        }

        private async Task<string> SaveImageAsync(IFormFile uploadedImage, string directoryName)
        {
            string fileFormat = "";

            if (uploadedImage.ContentType == "image/jpeg")
            {
                fileFormat = ".jpg";
            }
            else
            {
                fileFormat = ".png";
            }

            var imageName = Guid.NewGuid().ToString() + fileFormat;
            string path = $"\\images\\{directoryName}\\" + imageName;

            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            {
                await uploadedImage.CopyToAsync(fileStream);
            }

            return path;

        }

        public void DeleteImageFromFileSystemAsync(string path)
        {
            Task task = Task.Run(() => DeleteImageFile(path));
        }
        private void DeleteImageFile(string path)
        {
            FileInfo fileInfo = new(_appEnvironment.WebRootPath + path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
        }
    }
}
