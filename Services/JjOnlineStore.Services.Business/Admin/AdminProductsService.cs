using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Business._Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Optional;
using Optional.Collections;

using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Core.Admin;

using File = JjOnlineStore.Data.Entities.File;
using static System.IO.Path;

namespace JjOnlineStore.Services.Business.Admin
{
    public class AdminProductsService : BaseService, IAdminProductsService
    {
        private readonly IImageStorageService _imageStorageService;
        private readonly IFileService _fileService;

        public AdminProductsService(
            JjOnlineStoreDbContext dbContext,
            IMapper mapper, 
            IImageStorageService imageStorageService, 
            IFileService fileService) 
            : base(dbContext)
        {
            Mapper = mapper;
            _imageStorageService = imageStorageService;
            _fileService = fileService;
        }

        protected IMapper Mapper { get; }

        public async Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync()
            => await DbContext
                .Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task CreateAsync(ProductViewModel model)
        {
            var product = Mapper.Map<Product>(model);
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
            await SaveImagesForCurrentProductAsync(product, await SaveImageFilesAsync(model.FormImages));
        }

        private async Task SaveImagesForCurrentProductAsync(
            Product product,
            IEnumerable<File> images)
        {
            var productImages = images
                .Select(imgFile => new ProductImage(
                    fileId: imgFile.Id,
                    productId: product.Id));

            await DbContext.ProductImages.AddRangeAsync(productImages);
            await DbContext.SaveChangesAsync();
        }

        private async Task<IEnumerable<File>> SaveImageFilesAsync(IEnumerable<IFormFile> files)
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
                    resultCollection.Add(Option.None<string, Error>("Empty file.".ToError()));
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