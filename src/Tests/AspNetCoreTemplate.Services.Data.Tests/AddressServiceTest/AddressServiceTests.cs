namespace AspNetCoreTemplate.Services.Data.Tests.AddressServiceTest
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Data.Address;
    using Moq;
    using Xunit;

    public class AddressServiceTests
    {
        [Fact]
        public async Task CretaAsyncAddressShouldAddOneEntity()
        {
            var list = new List<Address>();
            var mockAddressRepository = new Mock<IRepository<Address>>();
            mockAddressRepository.Setup(x => x.All()).Returns(list.AsQueryable());
            mockAddressRepository.Setup(x => x.AddAsync(It.IsAny<Address>())).Callback((Address address) => list.Add(address));

            var service = new AddressService(mockAddressRepository.Object);

            await service.CreateAsyncAddress("1", "1", "1", "1");
            mockAddressRepository.Verify(x => x.AddAsync(It.IsAny<Address>()), Times.Exactly(1));
            Assert.Single(list);
        }
    }
}
