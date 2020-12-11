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
    using AspNetCoreTemplate.Web.ViewModels.Streets;
    using Moq;
    using Xunit;

    public class StrettsServiceTest
    {
        [Fact]
        public async Task CreateAsyncStreetsShouldCorrectCreateStreets()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listStreets = new List<Street>();
            var mockStreets = new Mock<IDeletableEntityRepository<Street>>();
            mockStreets.Setup(x => x.All()).Returns(listStreets.AsQueryable());
            mockStreets.Setup(x => x.AddAsync(It.IsAny<Street>())).Callback(
                (Street street) => listStreets.Add(street));

            var service = new StreetsService(mockStreets.Object);

            await service.CreateAsyncStreet("Street", "1");
            await service.CreateAsyncStreet("Street2", "1");
            await service.CreateAsyncStreet("Street3", "1");

            Assert.Equal(3, listStreets.Count);
        }

        [Fact]
        public async Task GetAllStreetsShouldReturnCorrectCOunt()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listStreets = new List<Street>();
            var mockStreets = new Mock<IDeletableEntityRepository<Street>>();
            mockStreets.Setup(x => x.All()).Returns(listStreets.AsQueryable());
            mockStreets.Setup(x => x.AddAsync(It.IsAny<Street>())).Callback(
                (Street street) => listStreets.Add(street));

            var service = new StreetsService(mockStreets.Object);

            await service.CreateAsyncStreet("Street", "1");
            await service.CreateAsyncStreet("Street2", "1");
            await service.CreateAsyncStreet("Street3", "2");

            Assert.Equal(3, listStreets.Count);

            var result = service.GetAllStreets<StreetsAll>("1");

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdStreetsShouldReturnCorrectStreet()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listStreets = new List<Street>();
            var mockStreets = new Mock<IDeletableEntityRepository<Street>>();
            mockStreets.Setup(x => x.All()).Returns(listStreets.AsQueryable());
            mockStreets.Setup(x => x.AddAsync(It.IsAny<Street>())).Callback(
                (Street street) => listStreets.Add(street));

            var service = new StreetsService(mockStreets.Object);

            var result = await service.CreateAsyncStreet("Street", "1");
            await service.CreateAsyncStreet("Street2", "1");
            await service.CreateAsyncStreet("Street3", "2");

            Assert.Equal(3, listStreets.Count);

            var result1 = service.GetById<StreetsAll>(result);
            Assert.Equal("Street", result1.Name);
            Assert.Equal("1", result1.AreaId);
        }

        [Fact]
        public async Task GetByNameStreetsShouldReturnCorrectStreet()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listStreets = new List<Street>();
            var mockStreets = new Mock<IDeletableEntityRepository<Street>>();
            mockStreets.Setup(x => x.All()).Returns(listStreets.AsQueryable());
            mockStreets.Setup(x => x.AddAsync(It.IsAny<Street>())).Callback(
                (Street street) => listStreets.Add(street));

            var service = new StreetsService(mockStreets.Object);

            var result = await service.CreateAsyncStreet("Street", "1");
            await service.CreateAsyncStreet("Street2", "1");
            await service.CreateAsyncStreet("Street3", "2");

            Assert.Equal(3, listStreets.Count);

            var result1 = service.GetByName<StreetsAll>("Street");

            Assert.Equal("Street", result1.Name);
            Assert.Equal("1", result1.AreaId);
        }
    }
}
