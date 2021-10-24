using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShopApp.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ShopApp.Interfaces;

namespace ShopApp.Repositories
{
    public class FileSystemRepository
    {
        private readonly IWebHostEnvironment _appEnvironment;
        
        public FileSystemRepository(IWebHostEnvironment appEnvironment)
        {
            this._appEnvironment = appEnvironment;
        }

        public async Task<string> SaveProductImageInFileSystemAsync(IFormFile uploadedImage)
        {
            string directoryName = "products";

            await Task.Run(() => CheckDirectoryExistence(directoryName));

            return await SaveImageAsync(uploadedImage, directoryName);
        }

        public async Task<string> SaveCategoryImageInFileSystemAsync(IFormFile uploadedImage)
        {
            string directoryName = "categories";

            await Task.Run(() => CheckDirectoryExistence(directoryName));

            return await SaveImageAsync(uploadedImage, directoryName);
        }
        private void CheckDirectoryExistence(string directoryName)
        {
            string directoryPath = _appEnvironment.WebRootPath + $"\\images\\{directoryName}";
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        private async Task<string> SaveImageAsync(IFormFile uploadedImage, string directoryName)
        {
            string fileFormat;
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
