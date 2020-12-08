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
    using AspNetCoreTemplate.Web.ViewModels.Locations;
    using Moq;
    using Xunit;

    public class LocationsServiceTest
    {
        [Fact]
        public async Task CreateAsyncLocationShouldCorrectCreateLocation()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            await service.CreateAsyncLocation(1, 1, 1, 1, "1");

            mockLO.Verify(x => x.AddAsync(It.IsAny<Location>()), Times.Exactly(1));
            Assert.Single(listLocation);
        }

        [Fact]
        public async Task GetAllLocationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            await service.CreateAsyncLocation(1, 1, 1, 1, "1");
            await service.CreateAsyncLocation(2, 2, 2, 2, "1");

            var result = service.GetAllLocations<LocationDetailsViewModel>("1");
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByNumberLocationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            await service.CreateAsyncLocation(1, 1, 1, 1, "1");
            await service.CreateAsyncLocation(2, 2, 2, 2, "1");

            var result = service.GetByNumber<LocationDetailsViewModel>(1);
            Assert.Equal(1, result.Number);
            Assert.Equal(1, result.Entrance);
            Assert.Equal(1, result.Flour);
            Assert.Equal(1, result.Apartament);
        }

        [Fact]
        public async Task GetByIdLocationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            var location = await service.CreateAsyncLocation(1, 1, 1, 1, "1");
            await service.CreateAsyncLocation(2, 2, 2, 2, "1");

            var result = service.GetById<LocationDetailsViewModel>(location);
            Assert.Equal(1, result.Number);
            Assert.Equal(1, result.Entrance);
            Assert.Equal(1, result.Flour);
            Assert.Equal(1, result.Apartament);
        }

        [Fact]
        public async Task GetByFlourLocationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            var location = await service.CreateAsyncLocation(1, 1, 1, 1, "1");
            await service.CreateAsyncLocation(2, 2, 2, 2, "1");

            var result = service.GetByFlour<LocationDetailsViewModel>(1);
            Assert.Equal(1, result.Number);
            Assert.Equal(1, result.Entrance);
            Assert.Equal(1, result.Flour);
            Assert.Equal(1, result.Apartament);
        }

        [Fact]
        public async Task GetByEntranceLocationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            var location = await service.CreateAsyncLocation(1, 1, 1, 1, "1");
            await service.CreateAsyncLocation(2, 2, 2, 2, "1");

            var result = service.GetByEntrance<LocationDetailsViewModel>(1);
            Assert.Equal(1, result.Number);
            Assert.Equal(1, result.Entrance);
            Assert.Equal(1, result.Flour);
            Assert.Equal(1, result.Apartament);
        }

        [Fact]
        public async Task GetByApartamentLocationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listLocation = new List<Location>();
            var mockLO = new Mock<IDeletableEntityRepository<Location>>();
            mockLO.Setup(x => x.All()).Returns(listLocation.AsQueryable());
            mockLO.Setup(x => x.AddAsync(It.IsAny<Location>())).Callback(
                (Location locationObject) => listLocation.Add(locationObject));

            var service = new LocationsService(mockLO.Object);
            var address = new Address { CityId = "1", AreaId = "1", StreetId = "1", LocationId = "1" };

            var location = await service.CreateAsyncLocation(1, 1, 1, 1, "1");
            await service.CreateAsyncLocation(2, 2, 2, 2, "1");

            var result = service.GetByApartament<LocationDetailsViewModel>(1);
            Assert.Equal(1, result.Number);
            Assert.Equal(1, result.Entrance);
            Assert.Equal(1, result.Flour);
            Assert.Equal(1, result.Apartament);
        }
    }
}
