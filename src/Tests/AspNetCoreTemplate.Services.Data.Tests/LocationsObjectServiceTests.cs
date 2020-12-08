namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using Moq;
    using Xunit;

    public class LocationsObjectServiceTests
    {
        [Fact]
        public async Task CreateAsyncLocationObjectShouldCorrectCreateLocationObject()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            await service.CreateAsyncLocationObject("Home", address.Id, "1");

            Assert.Single(listLocationObj);
            mockLO.Verify(x => x.AddAsync(It.IsAny<LocationObject>()), Times.Exactly(1));
        }

        [Fact]
        public async Task GetAllLocationsObjectsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "1");

            var result = service.GetAllLocationsObjects<LocationObjectIndexViewModel>();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllByUserIdObjectsShouldReturnCorrectLocations()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "2");
            await service.CreateAsyncLocationObject("Home3", address2.Id, "2");

            var result = service.GetAllByUserId<LocationObjectIndexViewModel>("1");
            var result2 = service.GetAllByUserId<LocationObjectIndexViewModel>("2");
            Assert.Single(result);
            Assert.Equal(2, result2.Count());
        }

        [Fact]
        public async Task GetAllByAddressIdShouldReturnCorrectLocations()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "2");
            await service.CreateAsyncLocationObject("Home3", address2.Id, "2");

            var result = service.GetAllByAddressId<LocationObjectIndexViewModel>(address.Id);
            var result2 = service.GetAllByAddressId<LocationObjectIndexViewModel>(address2.Id);
            Assert.Single(result);
            Assert.Equal(2, result2.Count());
        }

        [Fact]
        public async Task GetByNameShouldReturnCorrectLocations()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "2");
            await service.CreateAsyncLocationObject("Home3", address2.Id, "2");

            var result = service.GetByName<LocationObjectIndexViewModel>("Home");
            Assert.Equal("Home", result.Name);
            Assert.Equal(address.Id, result.AddressId);
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectLocations()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            var locId = await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "2");
            await service.CreateAsyncLocationObject("Home3", address2.Id, "2");

            var result = service.GetById<LocationObjectIndexViewModel>(locId);
            Assert.Equal("Home", result.Name);
            Assert.Equal(address.Id, result.AddressId);
        }

        [Fact]
        public async Task GetByUserIdShouldReturnCorrectLocations()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            var locId = await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "2");
            await service.CreateAsyncLocationObject("Home3", address2.Id, "2");

            var result = service.GetLocationByUserId<LocationObjectIndexViewModel>("1", "Home");
            Assert.Equal("Home", result.Name);
            Assert.Equal(address.Id, result.AddressId);
        }

        [Fact]
        public async Task GetByUserIdOnlyShouldReturnCorrectLocations()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocationObj = new List<LocationObject>();
            var mockLO = new Mock<IDeletableEntityRepository<LocationObject>>();
            mockLO.Setup(x => x.All()).Returns(listLocationObj.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<LocationObject>())).Callback(
                (LocationObject locationObject) => listLocationObj.Add(locationObject));

            var service = new LocationsObjectService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };
            var address2 = new Address { CityId = "2", AreaId = "2", StreetId = "2", LocationId = "2" };
            var locId = await service.CreateAsyncLocationObject("Home", address.Id, "1");
            await service.CreateAsyncLocationObject("Home2", address2.Id, "2");
            await service.CreateAsyncLocationObject("Home3", address2.Id, "2");

            var result = service.GetLocationByUserIdOnly<LocationObjectIndexViewModel>("1");
            Assert.Equal("Home", result.Name);
            Assert.Equal(address.Id, result.AddressId);
        }
    }
}
