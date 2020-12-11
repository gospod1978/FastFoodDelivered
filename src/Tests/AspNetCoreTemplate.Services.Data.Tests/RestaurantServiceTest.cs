namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using Moq;
    using Xunit;

    public class RestaurantServiceTest
    {
        [Fact]
        public async Task CreateAsyncRestaurantShouldCreateCorrectRestaurant()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var restaurant1 = await service.CreateAsync("image1", "898", "1", "1", "1");
            var restaurant2 = await service.CreateAsync("image2", "899", "1", "2", "1");
            var restaurant3 = await service.CreateAsync("image3", "897", "1", "3", "1");

            Assert.Equal(3, listRest.Count);
        }

        [Fact]
        public async Task CreateAsyncWorkingRestaurantShouldCreateCorrectRestaurant()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var restaurant1 = await service.CreateAsync("image1", "898", "11", "11", "11");
            var restaurant2 = await service.CreateAsync("image2", "899", "12", "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", "13", "13", "13");

            Assert.Equal(3, listRest.Count);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = service.CreateWorkingAreaByRestaurantId(restaurant3);
            foreach (var item in listRest)
            {
                Assert.Equal("Yes", item.IsWorking.ToString());
            }
        }

        [Fact]
        public async Task ChangeWorkingAreByRestaurantIdShouldUpdateInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, "11", "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");

            foreach (var item in listRest)
            {
                Assert.Equal("Yes", item.IsWorking.ToString());

                var workingAS = addressService.GetByWorkingAreaByUserId(item.UserId);

                Assert.Equal("12", workingAS.AreaId);
            }
        }

        [Fact]
        public async Task GetAllRestauranttShouldReturnCorectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, "11", "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");
            var result = service.GetAll<RestaurantAll>();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllRestauranttNoShouldReturnCorectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, "11", "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");
            service.MadeIsRestaurant(restaurant1);
            var result = service.GetAllNo<RestaurantAll>();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllRestauranttYesShouldReturnCorectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, "11", "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");
            service.MadeIsRestaurant(restaurant1);
            service.MadeIsRestaurant(restaurant2);
            var result = service.GetAllYes<RestaurantAll>();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdRestaurantShouldReturnCorectRestaurant()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, "11", "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");
            service.MadeIsRestaurant(restaurant1);
            service.MadeIsRestaurant(restaurant2);
            var result = service.GetById<RestaurantAll>(restaurant1);
            Assert.Equal("898", result.Phone);
        }

        [Fact]
        public async Task MadeIsRestaurantShouldReturnCorectRestaurant()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");
            var user = new ApplicationUser
            {
                Id = "11",
                UserName = "Ivan",
            };

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, user.Id, "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");
            service.MadeIsRestaurant(restaurant1);
            service.MadeIsRestaurant(restaurant2);
            var result = service.GetById<RestaurantWaitApprove>(restaurant1);
            Assert.Equal("Yes", result.IsRestaurnat.ToString());
        }

        [Fact]
        public async Task MadeIsNoRestaurantShouldReturnCorectRestaurant()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRest = new List<Restaurant>();
            var mockRestaurant = new Mock<IDeletableEntityRepository<Restaurant>>();
            mockRestaurant.Setup(x => x.All()).Returns(listRest.AsQueryable());
            mockRestaurant.Setup(x => x.AddAsync(It.IsAny<Restaurant>())).Callback(
                (Restaurant restaurant) => listRest.Add(restaurant));

            var listWorkingArea = new List<WorkingArea>();
            var mockWorkingArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWorkingArea.Setup(x => x.All()).Returns(listWorkingArea.AsQueryable());
            mockWorkingArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWorkingArea.Add(workingArea));
            var addressService = this.AddressService();
            var service = new RestaurantService(mockRestaurant.Object, addressService, mockWorkingArea.Object);

            var workingArea1 = await addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = await addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = await addressService.CreateAsyncWorkingArea("13", "13");
            var user = new ApplicationUser
            {
                Id = "11",
                UserName = "Ivan",
            };

            var restaurant1 = await service.CreateAsync("image1", "898", workingArea1, user.Id, "11");
            var restaurant2 = await service.CreateAsync("image2", "899", workingArea2, "12", "12");
            var restaurant3 = await service.CreateAsync("image3", "897", workingArea3, "13", "13");

            Assert.Equal(3, listRest.Count);

            var test = await service.CreateWorkingAreaByRestaurantId(restaurant1);
            var test1 = await service.CreateWorkingAreaByRestaurantId(restaurant2);
            var test2 = await service.CreateWorkingAreaByRestaurantId(restaurant3);
            var restTest = await service.ChangeWorkingAreaByRestaurantId(test, "12");
            var restTest1 = await service.ChangeWorkingAreaByRestaurantId(test2, "12");
            var restTest2 = await service.ChangeWorkingAreaByRestaurantId(test1, "12");
            service.MadeIsRestaurant(restaurant1);
            service.MadeIsNoRestaurant(restaurant1);
            var result = service.GetById<RestaurantWaitApprove>(restaurant1);
            Assert.Equal("No", result.IsRestaurnat.ToString());
        }

        private IAddressService AddressService()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listAddress = new List<Address>();
            var mockAddress = new Mock<IDeletableEntityRepository<Address>>();
            mockAddress.Setup(x => x.All()).Returns(listAddress.AsQueryable());
            mockAddress.Setup(x => x.AddAsync(It.IsAny<Address>())).Callback(
                (Address address) => listAddress.Add(address));

            var listWArea = new List<WorkingArea>();
            var mockWArea = new Mock<IDeletableEntityRepository<WorkingArea>>();
            mockWArea.Setup(x => x.All()).Returns(listWArea.AsQueryable());
            mockWArea.Setup(x => x.AddAsync(It.IsAny<WorkingArea>())).Callback(
                (WorkingArea workingArea) => listWArea.Add(workingArea));

            var service = new AddressService(mockAddress.Object, mockWArea.Object);

            return service;
        }
    }
}
