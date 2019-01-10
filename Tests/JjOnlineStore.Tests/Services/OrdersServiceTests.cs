using System.Threading.Tasks;
using JjOnlineStore.Data.EF;
using JjOnlineStore.Services.Business;
using JjOnlineStore.Services.Core;

using Moq;

using AutoMapper;
using JjOnlineStore.Common.ViewModels.Orders;
using JjOnlineStore.Data.Entities;
using JjOnlineStore.Tests.Attributes;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;
using static JjOnlineStore.Tests.DbContextProvider;


namespace JjOnlineStore.Tests.Services
{
    public class OrdersServiceTests
    {
        private readonly OrdersService _ordersService;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IOrderItemsService> _orderItemsServiceMock;
        private readonly JjOnlineStoreDbContext _dbContext;

        public OrdersServiceTests()
        {
            _dbContext = GetInMemoryDbContext();
            _mapperMock = new Mock<IMapper>();
            _orderItemsServiceMock = new Mock<IOrderItemsService>();
            _ordersService = new OrdersService(
                _dbContext,
                _mapperMock.Object,
                _orderItemsServiceMock.Object);
        }

        [Theory]
        [CustomAutoData]
        public async Task GetByIdAsync_Returns_Correct_Data(Order order)
        {
            // Arrange
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var orderVm = new OrderVm();

            MockMapper(order,orderVm);

            // Act
            var result = await _ordersService.GetByIdAsync(order.Id);

            // Assert
            result.ShouldBeNull();
        }

        private void MockMapper<T, TExpected>(T model, TExpected expected) =>
            _mapperMock.Setup(mapper => mapper
                    .Map<TExpected>(model))
                .Returns(expected);
    }
}