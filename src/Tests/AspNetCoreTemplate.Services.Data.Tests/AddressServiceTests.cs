namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using Moq;
    using Xunit;

    public class AddressServiceTests
    {
        [Fact]
        public async Task CreateAsyncAddressShouldCorrectCreateAddress()
        {
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

            mockWArea.Verify(x => x.AddAsync(It.IsAny<WorkingArea>()), Times.Exactly(1));
        }

        [Fact]
        public async Task GetAddressByIdShouldReturnCorrectInfo()
        {
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
            Assert.Equal("1", listAddress[0].CityId);
            Assert.Equal("1", listAddress[0].AreaId);
        }
    }
}
