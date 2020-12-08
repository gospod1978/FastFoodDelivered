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
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using Moq;
    using Xunit;

    public class CitiesServiceTests
    {
        [Fact]
        public async Task CreateAsyncCityShouldCorrectCreateCity()
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

            var service = new CitiesService(mockCity.Object, mockArea.Object);

            var city = await service.CreateAsyncCity("Sofia");
            await service.CreateAsyncCity("Plovidv");

            Assert.Equal(2, listCity.Count);
            mockCity.Verify(x => x.AddAsync(It.IsAny<City>()), Times.Exactly(2));
        }

        [Fact]
        public async Task GetCityNameByAreaIdShouldReturnCorrectCity()
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

            var service = new CitiesService(mockCity.Object, mockArea.Object);

            var service2 = new AreasService(mockArea.Object, mockCity.Object);

            var city = await service.CreateAsyncCity("Sofia");
            await service.CreateAsyncCity("Plovidv");

            var area = await service2.CreateAsyncArea("1", city, "1");

            var result = service.GetCityNameByAreaId(area);
            var result2 = service.GetById<CitiesAll>(city);
            Assert.Equal("SOFIA", result2.CityName);
            Assert.Equal("SOFIA", result);
        }

        [Fact]
        public async Task GetAllCitiesShoulReturnCorectCount()
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

            var service = new CitiesService(mockCity.Object, mockArea.Object);

            await service.CreateAsyncCity("Sofia");
            await service.CreateAsyncCity("Plovidv");

            var result = service.GetAllCities<CitiesAll>();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCityByIdShoulReturnCorectCity()
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

            var service = new CitiesService(mockCity.Object, mockArea.Object);

            var city1 = await service.CreateAsyncCity("Sofia");
            var city2 = await service.CreateAsyncCity("Plovidv");

            var result = service.GetById<CitiesAll>(city1);
            var result2 = service.GetById<CitiesAll>(city2);
            Assert.Equal(city1, result.Id);
            Assert.Equal(city2, result2.Id);
            Assert.Equal("Sofia".ToUpper(), result.CityName);
            Assert.Equal("Plovidv".ToUpper(), result2.CityName);
        }

        [Fact]
        public async Task GetCityByCityNameShoulReturnCorectCity()
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

            var service = new CitiesService(mockCity.Object, mockArea.Object);

            var city1 = await service.CreateAsyncCity("Sofia");
            var city2 = await service.CreateAsyncCity("Plovidv");

            var result = service.GetByName<CitiesAll>("Sofia".ToUpper());
            var result2 = service.GetByName<CitiesAll>("Plovidv".ToUpper());
            Assert.Equal(city1, result.Id);
            Assert.Equal(city2, result2.Id);
            Assert.Equal("Sofia".ToUpper(), result.CityName);
            Assert.Equal("Plovidv".ToUpper(), result2.CityName);
        }
    }
}
