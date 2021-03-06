﻿using JjOnlineStore.Common.Enumeration;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Extensions;

using Optional;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JjOnlineStore.Services.Business
{
    public class ProductsService : BaseService, IProductsService
    {
        public ProductsService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        public async Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync()
            => await DbContext
                .Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<IEnumerable<ProductViewModel>> GetByMainCategoryAsync(MainStoreCategories category) =>
            await DbContext
                .Products
                .Include(p => p.Category)
                .Where(p => p.Category.StoreCategory == category)
                .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();

        public async Task<Option<ProductViewModel, Error>> GetByIdAsync(long id)
            => await DbContext
            .Products
            .Include(p => p.Category)
            .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(p => p.Id == id)
            .SomeNotNull($"There is no product with id = {id}.".ToError());

        public async Task<IEnumerable<ProductViewModel>> GetByMainCategoryAndWordAsync(
            MainStoreCategories category,
            string searchedWord)
        {
            if (category == MainStoreCategories.All)
            {
                return await DbContext.Products
                    .Include(p => p.Category)
                    .Where(p => p.Name
                        .ToLower()
                        .Contains(searchedWord.ToLower()))
                    .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                    .ToListAsync();
            }

            return await DbContext.Products
                .Include(p => p.Category)
                .Where(p => p.Category.StoreCategory == category &&
                            p.Name.ToLower().Contains(searchedWord.ToLower()))
                .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                .ToListAsync();
        }

    }
}