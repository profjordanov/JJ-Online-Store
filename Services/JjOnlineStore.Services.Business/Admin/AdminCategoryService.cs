using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Admin.Categories;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core.Admin;
using Microsoft.EntityFrameworkCore;
using Optional;
using static Optional.Option;

namespace JjOnlineStore.Services.Business.Admin
{
    public class AdminCategoryService : BaseService, IAdminCategoryService
    {
        public AdminCategoryService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        public async Task<IEnumerable<CategoryViewModel>> AllAsync()
            => await DbContext
                .Categories
                .ProjectTo<CategoryViewModel>(Mapper.ConfigurationProvider) 
                .ToListAsync();

        public async Task<Option<CategoryViewModel, Error>> CreateCategoryAsync(CategoryViewModel model)
            => await ExistsByNameAsync(model.Name)
                ? None<CategoryViewModel, Error>(new Error($"Category '{model.Name}' already exists."))
                : (await CreateByViewModelAsync(model)).Some<CategoryViewModel, Error>();

        private async Task<CategoryViewModel> CreateByViewModelAsync(CategoryViewModel model)
        {
            var category = Mapper.Map<CategoryViewModel, Category>(model);
            await DbContext.Categories.AddAsync(category);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<CategoryViewModel>(category);
        }

        private async Task<bool> ExistsByNameAsync(string name)
            => await DbContext
                .Categories
                .AnyAsync(c => c.Name == name);
    }
}