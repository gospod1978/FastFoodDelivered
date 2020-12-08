namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;
    using Moq;
    using Xunit;

    public class AddressServiceTests
    {
        [Fact]
        public async Task CreateAsyncAddressShouldCorrectCreateAddress()
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

            await service.CreateAsyncAddress("1", "1", "1", "1");
            await service.CreateAsyncAddress("1", "2", "2", "2");
            await service.CreateAsyncAddress("1", "3", "3", "3");

            Assert.Equal(3, listAddress.Count);
        }

        [Fact]
        public async Task CreateWorkingAreaShouldCreateCorrectInfo()
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

            await service.CreateAsyncWorkingArea("1", "1");
            var area = service.GetByWorkingAreaByUserId("1");
            Assert.Equal("1", area.UserId);
            mockWArea.Verify(x => x.AddAsync(It.IsAny<WorkingArea>()), Times.Exactly(1));
        }

        [Fact]
        public async Task GetWorkingAreaByUserIdShouldReturnCorrectInformacion()
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

            await service.CreateAsyncWorkingArea("1", "1");
            await service.CreateAsyncWorkingArea("2", "2");
            var area = service.GetByWorkingAreaByUserId("1");
            var area2 = service.GetByWorkingAreaByUserId("2");
            Assert.Equal("1", area.UserId);
            Assert.Equal("2", area2.UserId);
            mockWArea.Verify(x => x.AddAsync(It.IsAny<WorkingArea>()), Times.Exactly(2));
        }

        [Fact]
        public async Task ChangeWorkingAreaToBeOnNeedChangeInformation()
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

            var area = await service.CreateAsyncWorkingArea("1", "1");
            service.ChangeWorkingAreaOn(area, "1");
            var area2 = service.GetByWorkingAreaByUserId("1");

            Assert.Equal("Yes", area2.ActiveWorkingArea.ToString());
        }

        [Fact]
        public async Task ChangeWorkingAreaToBeOffNeedChangeInformation()
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

            var area = await service.CreateAsyncWorkingArea("1", "1");
            service.ChangeWorkingAreaOn(area, "1");
            var area2 = service.GetByWorkingAreaByUserId("1");

            Assert.Equal("Yes", area2.ActiveWorkingArea.ToString());

            service.ChangeWorkingAreaOff(area);
            area2 = service.GetByWorkingAreaByUserId("1");

            Assert.Equal("No", area2.ActiveWorkingArea.ToString());
        }

        [Fact]
        public async Task GetAddressByIdShouldReturnCorrectInfo()
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

            var addressId = await service.CreateAsyncAddress("1", "1", "1", "1");

            var result = service.GetById<IndexAddressViewModel>(addressId);
            Assert.NotNull(result);
            Assert.Equal("1", result.AreaId);
            Assert.Equal("1", result.CityId);
            Assert.Equal("1", result.StreetId);
            Assert.Equal("1", result.LocationId);
        }

        [Fact]
        public async Task GetAddressByIdShouldReturnCorrectInfook()
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

            var addressId = await service.CreateAsyncAddress("1", "1", "1", "1");

            var result = service.GetAddress(addressId);

            Assert.NotNull(result);
            Assert.Equal("1", result.AreaId);
            Assert.Equal("1", result.CityId);
            Assert.Equal("1", result.StreetId);
            Assert.Equal("1", result.LocationId);
        }

        [Fact]
        public async void GetAllAddressShouldReturnCorrectCount()
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
            var city = new City { CityName = "Sofia" };

            await service.CreateAsyncAddress(city.Id, "1", "1", "1");
            await service.CreateAsyncAddress(city.Id, "1", "1", "2");
            await service.CreateAsyncAddress(city.Id, "1", "1", "3");

            Assert.Equal(3, listAddress.Count);

            var result = service.GetAllAddress<IndexAddressViewModel>();
            Assert.Equal(3, service.GetAllAddress<IndexAddressViewModel>().Count());
        }
    }
}
