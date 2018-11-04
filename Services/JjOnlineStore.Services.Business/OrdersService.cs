using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JjOnlineStore.Common.ViewModels.CartItems;
using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Common.ViewModels.Products;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;
using Microsoft.EntityFrameworkCore;

namespace JjOnlineStore.Services.Business
{
    public class OrdersService : BaseService, IOrdersService
    {
        public OrdersService(JjOnlineStoreDbContext dbContext, IMapper mapper) 
            : base(dbContext)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }

        public async Task<OrderVm> GetByIdAsync(long orderId)
        {
            var entity = await DbContext
                .Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            var viewModel = Mapper.Map<Order, OrderVm>(entity);

            //viewModel.Cart.CartItems =
            //    Mapper.Map<IEnumerable<CartItem>, IEnumerable<CartItemVm>>(entity.Cart.OrderedItems);

            foreach (var cartItem in viewModel.Cart.CartItems)
            {
                cartItem.Product = await DbContext
                    .Products
                    .Where(p => p.Id == cartItem.ProductId)
                    .ProjectTo<ProductViewModel>(Mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();
            }

            return viewModel;
        }

        public async Task<long> CreateAsync(OrderVm model)
        {
            var entity = Mapper.Map<OrderVm, Order>(model);
            await DbContext.Orders.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity.Id;
        }
    }
}