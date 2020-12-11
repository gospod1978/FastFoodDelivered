namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Vehicle;
    using Moq;
    using Xunit;

    public class VehichleServiceTests
    {
        [Fact]
        public async Task CreateVehichleStreetsShouldCorrectCreateVehichle()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listVehichle = new List<Vehichle>();
            var mockVehichle = new Mock<IDeletableEntityRepository<Vehichle>>();
            mockVehichle.Setup(x => x.All()).Returns(listVehichle.AsQueryable());
            mockVehichle.Setup(x => x.AddAsync(It.IsAny<Vehichle>())).Callback(
                (Vehichle vehichle) => listVehichle.Add(vehichle));

            var service = new VehicleService(mockVehichle.Object);

            var result = await service.CreateAsync("Car");
            await service.CreateAsync("Motor");
            await service.CreateAsync("Bus");

            Assert.Equal(3, listVehichle.Count);
        }

        [Fact]
        public async Task GetAllVehichleStreetsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listVehichle = new List<Vehichle>();
            var mockVehichle = new Mock<IDeletableEntityRepository<Vehichle>>();
            mockVehichle.Setup(x => x.All()).Returns(listVehichle.AsQueryable());
            mockVehichle.Setup(x => x.AddAsync(It.IsAny<Vehichle>())).Callback(
                (Vehichle vehichle) => listVehichle.Add(vehichle));

            var service = new VehicleService(mockVehichle.Object);

            var result = await service.CreateAsync("Car");
            await service.CreateAsync("Motor");
            await service.CreateAsync("Bus");

            Assert.Equal(3, listVehichle.Count);

            var result1 = service.GetAll<VehicleAll>();
            Assert.Equal(3, result1.Count());
        }

        [Fact]
        public async Task GetByIdVehichleShouldReturnCorrectVehichle()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listVehichle = new List<Vehichle>();
            var mockVehichle = new Mock<IDeletableEntityRepository<Vehichle>>();
            mockVehichle.Setup(x => x.All()).Returns(listVehichle.AsQueryable());
            mockVehichle.Setup(x => x.AddAsync(It.IsAny<Vehichle>())).Callback(
                (Vehichle vehichle) => listVehichle.Add(vehichle));

            var service = new VehicleService(mockVehichle.Object);

            var result = await service.CreateAsync("Car");
            await service.CreateAsync("Motor");
            await service.CreateAsync("Bus");

            Assert.Equal(3, listVehichle.Count);

            var result1 = service.GetById<VehicleAll>(result);
            Assert.Equal("Car", result1.Name);
            Assert.Equal(result, result1.Id);
        }

        [Fact]
        public async Task GetByNameVehichleShouldReturnCorrectVehichle()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listVehichle = new List<Vehichle>();
            var mockVehichle = new Mock<IDeletableEntityRepository<Vehichle>>();
            mockVehichle.Setup(x => x.All()).Returns(listVehichle.AsQueryable());
            mockVehichle.Setup(x => x.AddAsync(It.IsAny<Vehichle>())).Callback(
                (Vehichle vehichle) => listVehichle.Add(vehichle));

            var service = new VehicleService(mockVehichle.Object);

            var result = await service.CreateAsync("Car");
            await service.CreateAsync("Motor");
            await service.CreateAsync("Bus");

            Assert.Equal(3, listVehichle.Count);

            var result1 = service.GetByName<VehicleAll>("Car");
            Assert.Equal("Car", result1.Name);
            Assert.Equal(result, result1.Id);
        }
    }
}
