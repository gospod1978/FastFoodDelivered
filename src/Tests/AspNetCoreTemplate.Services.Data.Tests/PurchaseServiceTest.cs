namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.Orders;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Purchases;
    using Moq;
    using Xunit;

    public class PurchaseServiceTest
    {
        [Fact]
        public async Task CreateAsyncMenyShouldCreateMenu()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);

            Assert.Equal(3, listPurchase.Count);
        }

        [Fact]
        public async Task AddCourierToPurcahseShoulAddCorectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", null, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);

            Assert.Equal(3, listPurchase.Count);
            service.AddCourier(purchase1, courier.Id);
            foreach (var item in listPurchase)
            {
                Assert.Equal(courier.Id, item.CourierId);
            }
        }

        [Fact]
        public async Task ChangeStatusShouldUpdateCorrectPurchase()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "2", courier.Id, restaurant.Id, "0", 3.70M, 3.70M);

            Assert.Equal(3, listPurchase.Count);
            await service.ChangeStatus(purchase1, "1");
            await service.ChangeStatus(purchase2, "1");
            await service.ChangeStatus(purchase3, "1");
            foreach (var item in listPurchase)
            {
                var newStatus = (Status)Enum.Parse(typeof(Status), "1");
                Assert.Equal(newStatus, item.Status);
            }
        }

        [Fact]
        public async Task GetAllByCourierIdShouldReturnCorrectCourier()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, restaurant.Id, "0", 3.70M, 3.70M);

            Assert.Equal(3, listPurchase.Count);
            var courierFind = service.GetAllByCourierId<TestPurchaseDetails>(courier.Id);
            Assert.Equal(3, courierFind.Count());
        }

        [Fact]
        public async Task GetAllByRestaurantIdShouldReturnCorrectPurchase()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, "2", "1", 3.70M, 3.70M);

            Assert.Equal(3, listPurchase.Count);
            var purchaseFind = service.GetAllByRestaurantId<TestPurchaseDetails>("1");
            Assert.Equal(2, purchaseFind.Count());
        }

        [Fact]
        public async Task GetAllByUserIdShouldReturnCorrectPurchase()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "2", courier.Id, restaurant.Id, "1", 3.70M, 3.70M);

            Assert.Equal(3, listPurchase.Count);
            var purchaseFind = service.GetAllByUserId<TestPurchaseDetails>("1");
            Assert.Equal(2, purchaseFind.Count());
        }

        [Fact]
        public async Task GetAllByPromotionTypeShouldReturnCorrectPurchase()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, restaurant.Id, "1", 3.70M, 3.70M);

            Assert.Equal(3, listPurchase.Count);
            var purchaseFind = service.GetAllByPromotionType<TestPurchaseDetails>("0");
            Assert.Equal(2, purchaseFind.Count());
        }

        [Fact]
        public async Task GetAllByStatusShouldReturnCorrectPurchase()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, restaurant.Id, "1", 3.70M, 3.70M);
            await service.ChangeStatus(purchase1, "0");
            await service.ChangeStatus(purchase2, "0");
            await service.ChangeStatus(purchase3, "0");
            Assert.Equal(3, listPurchase.Count);
            var purchaseFind = service.GetAllByStatus<TestPurchaseDetails>("0");
            Assert.Equal(3, purchaseFind.Count());
        }

        [Fact]
        public async Task GetAllByIdShouldReturnCorrectPurchase()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPurchase = new List<Purchase>();
            var mockPurchase = new Mock<IDeletableEntityRepository<Purchase>>();
            mockPurchase.Setup(x => x.All()).Returns(listPurchase.AsQueryable());
            mockPurchase.Setup(x => x.AddAsync(It.IsAny<Purchase>())).Callback(
                (Purchase purchase) => listPurchase.Add(purchase));

            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));

            var listRestaurant = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRestaurant.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRestaurant.Add(restaurant));

            var courierService = this.CouierService();
            var service = new PurchaseService(mockPurchase.Object, mockCourier.Object, mockArea.Object, mockWorkingArea.Object, mockRestaurant.Object, courierService);
            var courier = this.Courier();
            var restaurant = this.Restaurant();
            var purchase1 = await service.CreateAsyncMenu("1", "1", courier.Id, restaurant.Id, "0", 3.50M, 3.90M);
            var purchase2 = await service.CreateAsyncMenu("2", "1", courier.Id, restaurant.Id, "0", 3.60M, 3.80M);
            var purchase3 = await service.CreateAsyncMenu("3", "1", courier.Id, restaurant.Id, "1", 3.70M, 3.70M);
            await service.ChangeStatus(purchase1, "0");
            await service.ChangeStatus(purchase2, "0");
            await service.ChangeStatus(purchase3, "0");
            Assert.Equal(3, listPurchase.Count);
            var purchaseFind = service.GetById<TestPurchaseDetails>(purchase1);
            Assert.Equal(courier.Id, purchaseFind.CourierId);
            Assert.Equal(3.50M, purchaseFind.MenuPrice);
        }

        private Courier Courier()
        {
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            var courier = new Courier
            {
                Id = "1",
                Image = "photo",
                Phone = "898",
                VehicleId = "1",
                Birthday = dt,
                WorkingAreaId = "1",
                IsWorking = IsWorking.Yes,
                IsCourier = IsCourier.Yes,
                UserId = "1",
                AreaId = "1",
            };

            return courier;
        }

        private Restaurant Restaurant()
        {
            var restaurant = new Restaurant
            {
                Id = "1",
                Image = "photo",
                Phone = "898",
                WorkingAreaId = "1",
                IsWorking = IsWorking.Yes,
                IsRestaurnat = IsRestaurnat.Yes,
                UserId = "1",
                AreaId = "1",
            };

            return restaurant;
        }

        private ICourierService CouierService()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCourier = new List<Courier>();
            var mockCourier = new Mock<IDeletableEntityRepository<Courier>>();
            mockCourier.Setup(x => x.All()).Returns(listCourier.AsQueryable());
            mockCourier.Setup(x => x.AddAsync(It.IsAny<Courier>())).Callback(
                (Courier courier) => listCourier.Add(courier));

            var listVehichle = new List<Vehichle>();
            var mockVehichle = new Mock<IDeletableEntityRepository<Vehichle>>();
            mockVehichle.Setup(x => x.All()).Returns(listVehichle.AsQueryable());
            mockVehichle.Setup(x => x.AddAsync(It.IsAny<Vehichle>())).Callback(
                (Vehichle vehichle) => listVehichle.Add(vehichle));

            var listWAR = new List<WorkingArea>();
            var mockWAR = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWAR.Setup(x => x.All()).Returns(listWAR.AsQueryable());
            mockWAR.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea war) => listWAR.Add(war));

            var listAddress = new List<Address>();
            var mockAddress = new Mock<IDeletableEntityRepository<Address>>();
            mockAddress.Setup(x => x.All()).Returns(listAddress.AsQueryable());
            mockAddress.Setup(x => x.AddAsync(It.IsAny<Address>())).Callback(
                (Address address) => listAddress.Add(address));

            var addressService = new AddressService(mockAddress.Object, mockWAR.Object);
            var service = new CourierService(mockCourier.Object, mockVehichle.Object, mockWAR.Object, addressService);

            return service;
        }
    }
}
