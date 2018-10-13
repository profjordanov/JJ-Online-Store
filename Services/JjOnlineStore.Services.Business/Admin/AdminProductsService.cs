using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core.Admin;
using Microsoft.EntityFrameworkCore;

namespace JjOnlineStore.Services.Business.Admin
{
    public class AdminProductsService : BaseService, IAdminProductsService
    {
        public AdminProductsService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        public async Task<IEnumerable<ProductViewModel>> AllWithoutDeletedAsync()
        {
            var query = await DbContext
                .Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.Category)
                .ToListAsync();

            var result = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

            return result;
        }
    }
}