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
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using Moq;
    using Xunit;

    public class AreasServiceTests
    {
        [Fact]
        public async Task CreateAsyncAreaShouldCorrectCreateArea()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCity = new List<City>();
            var mockCity = new Mock<IDeletableEntityRepository<City>>();
            mockCity.Setup(x => x.All()).Returns(listCity.AsQueryable());
            mockCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => listCity.Add(city));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var service = new AreasService(mockArea.Object, mockCity.Object);

            await service.CreateAsyncArea("1", "1", "1");
            await service.CreateAsyncArea("2", "1", "2");
            await service.CreateAsyncArea("3", "1", "3");

            Assert.Equal(3, listArea.Count);
            mockArea.Verify(x => x.AddAsync(It.IsAny<Area>()), Times.Exactly(3));
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectArea()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCity = new List<City>();
            var mockCity = new Mock<IDeletableEntityRepository<City>>();
            mockCity.Setup(x => x.All()).Returns(listCity.AsQueryable());
            mockCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => listCity.Add(city));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var service = new AreasService(mockArea.Object, mockCity.Object);

            var areaId = await service.CreateAsyncArea("Area", "1", "1");

            Assert.Equal("Area", service.GetById<AreasAll>(areaId).AreaName);
        }

        [Fact]
        public async Task GetCityByAreaIdShouldReturnCorrectArea()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCity = new List<City>();
            var mockCity = new Mock<IDeletableEntityRepository<City>>();
            mockCity.Setup(x => x.All()).Returns(listCity.AsQueryable());
            mockCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => listCity.Add(city));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var service = new AreasService(mockArea.Object, mockCity.Object);
            var city = new City { CityName = "Sofia" };
            var areaId = await service.CreateAsyncArea("Area", city.Id, "1");
            var result = service.GetCityByAreaId<AreasAll>(areaId);
            Assert.Equal(city.Id, result.CityId);
        }

        [Fact]
        public async Task GetByNameShouldReturnCorrectArea()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCity = new List<City>();
            var mockCity = new Mock<IDeletableEntityRepository<City>>();
            mockCity.Setup(x => x.All()).Returns(listCity.AsQueryable());
            mockCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => listCity.Add(city));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var service = new AreasService(mockArea.Object, mockCity.Object);

            var areaId = await service.CreateAsyncArea("Area", "1", "1");

            Assert.Equal("Area", service.GetByName<AreasAll>("Area").AreaName);
        }

        [Fact]
        public async Task GetAllWorkingAreasShouldReturnCorrectArea()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCity = new List<City>();
            var mockCity = new Mock<IDeletableEntityRepository<City>>();
            mockCity.Setup(x => x.All()).Returns(listCity.AsQueryable());
            mockCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => listCity.Add(city));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var service = new AreasService(mockArea.Object, mockCity.Object);
            var city = new City { CityName = "Sofia" };
            await service.CreateAsyncArea("Area", city.Id, "1");
            await service.CreateAsyncArea("Area2", city.Id, "2");
            var result = service.GetAllWorkingAreas<AreasAll>(city.Id);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllAreasShouldReturnCorrectArea()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCity = new List<City>();
            var mockCity = new Mock<IDeletableEntityRepository<City>>();
            mockCity.Setup(x => x.All()).Returns(listCity.AsQueryable());
            mockCity.Setup(x => x.AddAsync(It.IsAny<City>())).Callback(
                (City city) => listCity.Add(city));

            var listArea = new List<Area>();
            var mockArea = new Mock<IDeletableEntityRepository<Area>>();
            mockArea.Setup(x => x.All()).Returns(listArea.AsQueryable());
            mockArea.Setup(x => x.AddAsync(It.IsAny<Area>())).Callback(
                (Area area) => listArea.Add(area));

            var service = new AreasService(mockArea.Object, mockCity.Object);
            var city = new City { CityName = "Sofia" };
            await service.CreateAsyncArea("Area", city.Id, "1");
            await service.CreateAsyncArea("Area2", city.Id, "2");
            var result = service.GetAllAreas<AreasAll>(city.Id);

            Assert.Equal(2, result.Count());
        }
    }
}
