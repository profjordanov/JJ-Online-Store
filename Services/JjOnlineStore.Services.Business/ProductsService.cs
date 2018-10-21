using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using Microsoft.EntityFrameworkCore;

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
    }
}