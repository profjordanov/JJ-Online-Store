using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Extensions;
using JjOnlineStore.Services.Business._Base;
using Microsoft.EntityFrameworkCore;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Services.Core.Admin;

namespace JjOnlineStore.Services.Business.Admin
{
    public class AdminProductsService : BaseService, IAdminProductsService
    {
        private readonly IFileService _fileService;
        private readonly IProductImagesService _productImagesService;

        public AdminProductsService(
            JjOnlineStoreDbContext dbContext,
            IMapper mapper, 
            IFileService fileService, 
            IProductImagesService productImagesService) 
            : base(dbContext)
        {
            Mapper = mapper;
            _fileService = fileService;
            _productImagesService = productImagesService;
        }

        protected IMapper Mapper { get; }

        public async Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync()
            => await DbContext
                .Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

        /// <summary>
        /// Creates new product and save it's images.
        /// </summary>
        /// <param name="model">Product Model</param>
        public async Task CreateAsync(ProductViewModel model)
        {
            var product = Mapper.Map<Product>(model);
            await DbContext.Products.AddAsync(product);
            await DbContext.SaveChangesAsync();
            if (!model.FormImages.IsNullOrEmpty())
            {
                var savedImageFiles = await _fileService.SaveImageFilesAsync(model.FormImages);
                await _productImagesService.SaveImagesByProductAsync(product,savedImageFiles);
            }          
        }       
    }
}