namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Data.Orders;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Orders;
    using Moq;
    using Xunit;

    public class OrderServiceTest
    {
        [Fact]
        public async Task CreateOrderShouldCreateOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);

            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, 2);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "2", "11", 1.22M, 2);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, 3);

            Assert.Equal(3, listOrder.Count);
        }

        [Fact]
        public async Task GetByCategoryIdOrderShouldReturnCorrectCountOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);

            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, 2);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "2", "11", 1.22M, 2);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, 3);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetAllByCategoryId<OrderDetailsViewModel>(2);
            Assert.Equal(2, result1.Count());
        }

        [Fact]
        public async Task GetAllByPriceOrderShouldReturnCorrectCountOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "2", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetAllByPrice<OrderDetailsViewModel>(1.20M);
            Assert.Equal(2, result1.Count());
        }

        [Fact]
        public async Task GetAllByPromotionIdOrderShouldReturnCorrectCountOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "2", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetAllByPromotionId<OrderDetailsViewModel>("0");
            Assert.Equal(3, result1.Count());
        }

        [Fact]
        public async Task GetAllByPromotionNameOrderShouldReturnCorrectCountOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "2", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetAllByPromotionName<OrderDetailsViewModel>("NoPromotion");
            Assert.Equal(3, result1.Count());
        }

        [Fact]
        public async Task GetAllByRestaurantIdOrderShouldReturnCorrectCountOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "1", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetAllByRestaurantId<OrderDetailsViewModel>("1");
            Assert.Equal(2, result1.Count());
        }

        [Fact]
        public async Task GetAllOrdersShouldReturnCorrectCountOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "1", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetAllOrders<OrderDetailsViewModel>();
            Assert.Equal(3, result1.Count());
        }

        [Fact]
        public async Task GetByIdOrderShouldReturnCorrectOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "1", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetById<OrderDetailsViewModel>(result);
            Assert.Equal("Bar", result1.Name);
            Assert.Equal("Pizza", result1.Description);
        }

        [Fact]
        public async Task GetByNameOrderShouldReturnCorrectOrder()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listOrder = new List<Order>();
            var mockOrder = new Mock<IDeletableEntityRepository<Order>>();
            mockOrder.Setup(x => x.All()).Returns(listOrder.AsQueryable());
            mockOrder.Setup(x => x.AddAsync(It.IsAny<Order>())).Callback(
                (Order order) => listOrder.Add(order));

            var service = new OrdersService(mockOrder.Object);
            var category = new Category { Id = 2, Name = "Duner" };
            var result = await service.CreateAsyncMenu("Bar", "Pizza", "1", "10", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar1", "Pizza2", "1", "11", 1.20M, category.Id);
            await service.CreateAsyncMenu("Bar2", "Pizza2", "3", "12", 1.24M, category.Id);

            Assert.Equal(3, listOrder.Count);
            var result1 = service.GetByName<OrderDetailsViewModel>("Bar");
            Assert.Equal("Bar", result1.Name);
            Assert.Equal("Pizza", result1.Description);
        }
    }
}
