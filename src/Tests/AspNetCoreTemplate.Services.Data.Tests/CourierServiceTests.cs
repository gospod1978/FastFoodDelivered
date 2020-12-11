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
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using Moq;
    using Xunit;

    public class CourierServiceTests
    {
        [Fact]
        public async Task CreateCourierShouldCreateCourier()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");

            Assert.Equal(3, listCourier.Count);
        }

        [Fact]
        public async Task CreateWorkingAreaByCourierShouldCreateWAR()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            Assert.Equal(3, listCourier.Count);
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            foreach (var item in listCourier)
            {
                Assert.Equal("Yes", item.IsWorking.ToString());
            }
        }

        [Fact]
        public async Task GetAllCourierShouldReturnCorrectCount()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            Assert.Equal(3, listCourier.Count);

            var test = service.GetAll<CuriersAll>();
            Assert.Equal(3, test.Count());
        }

        [Fact]
        public async Task GetAllYesCourierShouldReturnCorrectCount()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            service.MadeIsCourier(result);
            service.MadeIsCourier(result2);
            Assert.Equal(3, listCourier.Count);

            var allYes = service.GetAllYes<CuriersAll>();
            Assert.Equal(2, allYes.Count());
        }

        [Fact]
        public async Task GetAllNoCourierShouldReturnCorrectCount()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            service.MadeIsCourier(result);
            Assert.Equal(3, listCourier.Count);

            var allNo = service.GetAllNo<CuriersAll>();
            Assert.Equal(2, allNo.Count());
        }

        [Fact]
        public async Task GetbyIdCourierShouldReturnCorrectCourier()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            service.MadeIsCourier(result);
            service.MadeIsCourier(result2);
            Assert.Equal(3, listCourier.Count);

            var courier = service.GetById<CuriersAll>(result);
            Assert.Equal("8881", courier.Phone);
        }

        [Fact]
        public async Task GetbyUserIdCourierShouldReturnCorrectCourier()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            service.MadeIsCourier(result);
            service.MadeIsCourier(result2);
            Assert.Equal(3, listCourier.Count);
            var testCourier = service.GetByUserId<CuriersAll>("11");
            Assert.Equal(result, testCourier.Id);
        }

        [Fact]
        public async Task MadeCourierCourierShouldReturnCorrectInfo()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            service.MadeIsCourier(result);
            service.MadeIsCourier(result2);
            Assert.Equal(3, listCourier.Count);

            var allYes = service.GetAllYes<CuriersAll>();
            Assert.Equal(2, allYes.Count());
        }

        [Fact]
        public async Task MadeNoCourierCourierShouldReturnCorrectInfo()
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
            DateTime dt = DateTime.Parse("10.08.1978", CultureInfo.InvariantCulture);
            DateTime dt1 = DateTime.Parse("10.08.1979", CultureInfo.CurrentCulture);
            DateTime dt2 = DateTime.Parse("10.08.1980", CultureInfo.CurrentCulture);
            var result = await service.CreateAsync("Image", "8881", "11", dt, "11", "11", "11");
            var result2 = await service.CreateAsync("Image2", "8882", "12", dt1, "12", "12", "12");
            var result3 = await service.CreateAsync("Image2", "8883", "13", dt2, "13", "13", "13");
            var workingArea1 = addressService.CreateAsyncWorkingArea("11", "11");
            var workingArea2 = addressService.CreateAsyncWorkingArea("12", "12");
            var workingArea3 = addressService.CreateAsyncWorkingArea("13", "13");
            var test = service.CreateWorkingAreaByCourierId(result, "12");
            var test1 = service.CreateWorkingAreaByCourierId(result2, "12");
            var test2 = service.CreateWorkingAreaByCourierId(result3, "12");
            service.MadeIsCourier(result);
            Assert.Equal(3, listCourier.Count);

            var allNo = service.GetAllNo<CuriersAll>();
            Assert.Equal(2, allNo.Count());
        }
    }
}
