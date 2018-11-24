using System.Collections.Generic;
using JjOnlineStore.Common.ViewModels;
using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Services.Business._Base;
using JjOnlineStore.Services.Core;

using Microsoft.EntityFrameworkCore;

using Optional;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using System.Linq;
using System.Threading.Tasks;

namespace JjOnlineStore.Services.Business
{
    public class OrdersService : BaseService, IOrdersService
    {
        private readonly IOrderItemsService _orderItemsService;
        public OrdersService(
            JjOnlineStoreDbContext dbContext,
            IMapper mapper,
            IOrderItemsService orderItemsService) 
            : base(dbContext)
        {
            Mapper = mapper;
            _orderItemsService = orderItemsService;
        }

        protected IMapper Mapper { get; }

        public async Task<OrderVm> GetByIdAsync(long orderId)
        {
            var entity = await DbContext
                .Orders
                .Where(o => o.Id == orderId)
                .Include(o => o.OrderedItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync();

            return Mapper.Map<Order, OrderVm>(entity);
        }

        public async Task<Option<long, Error>> CreateAsync(OrderVm model)
        {
            var entity = Mapper.Map<OrderVm, Order>(model);
            await DbContext.Orders.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return (await _orderItemsService.CreateRangeByUserIdAndOrderIdAsync(model.UserId, entity.Id))
                .FlatMap(oi => entity.Id.Some<long, Error>());
        }

        public IEnumerable<OrderVm> GetByUserId(string userId) =>
            DbContext
                .Orders
                .Where(o => o.UserId == userId)
                .ProjectTo<OrderVm>(Mapper.ConfigurationProvider);
    }
}