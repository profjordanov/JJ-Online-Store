using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using Microsoft.AspNetCore.Http;
using Optional;
using Optional.Collections;

using File = JjOnlineStore.Data.Entities.File;
using static System.IO.Path;

namespace JjOnlineStore.Services.Business
{
    public class FileService : BaseService, IFileService
    {
        private readonly IImageStorageService _imageStorageService;

        public FileService(JjOnlineStoreDbContext dbContext, IImageStorageService imageStorageService) 
            : base(dbContext)
        {
            _imageStorageService = imageStorageService;
        }

        /// <summary>
        /// Stores image files and adds data to the database.
        /// </summary>
        /// <param name="files"></param>
        /// <returns>Newly files entities.</returns>
        public async Task<IEnumerable<File>> SaveImageFilesAsync(IEnumerable<IFormFile> files)
        {
            var imagePaths = await StoreImagesAsync(files);
            var storedImages = imagePaths.Select(imgPath => new File(imgPath, GetExtension(imgPath)))
                .ToArray();
            await DbContext.Files.AddRangeAsync(storedImages);
            await DbContext.SaveChangesAsync();
            return storedImages;
        }

        private async Task<string[]> StoreImagesAsync(IEnumerable<IFormFile> files)
        {
            var resultCollection = new List<Option<string, Error>>();
            foreach (var formFile in files)
            {
                if (formFile.Length <= 0)
                {
                    continue;
                }

                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);
                    resultCollection.Add(
                        await _imageStorageService.StoreImageAsync(
                            formFile.FileName,
                            memoryStream.ToArray()));
                }
            }

            return resultCollection.Values().ToArray();
        }
    }
}