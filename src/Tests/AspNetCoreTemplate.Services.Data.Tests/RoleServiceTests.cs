namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Roles;
    using Moq;
    using Xunit;

    public class RoleServiceTests
    {
        [Fact]
        public async Task CreateRoleSHhouldCreateInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRole = new List<ApplicationRole>();
            var mockRole = new Mock<IDeletableEntityRepository<ApplicationRole>>();
            mockRole.Setup(x => x.All()).Returns(listRole.AsQueryable());
            mockRole.Setup(x => x.AddAsync(It.IsAny<ApplicationRole>())).Callback(
                (ApplicationRole role) => listRole.Add(role));
            var service = new RoleService(mockRole.Object);
            var role1 = await service.CreateAsyncRole("One");
            var role2 = await service.CreateAsyncRole("Two");
            var role3 = await service.CreateAsyncRole("Three");

            Assert.Equal(3, listRole.Count);
        }

        [Fact]
        public async Task GetAllRoleSHhouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRole = new List<ApplicationRole>();
            var mockRole = new Mock<IDeletableEntityRepository<ApplicationRole>>();
            mockRole.Setup(x => x.All()).Returns(listRole.AsQueryable());
            mockRole.Setup(x => x.AddAsync(It.IsAny<ApplicationRole>())).Callback(
                (ApplicationRole role) => listRole.Add(role));
            var service = new RoleService(mockRole.Object);
            var role1 = await service.CreateAsyncRole("One");
            var role2 = await service.CreateAsyncRole("Two");
            var role3 = await service.CreateAsyncRole("Three");

            Assert.Equal(3, listRole.Count);
            var result = service.GetAll<RoleAllViewModel>();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetByIdRoleSHhouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listRole = new List<ApplicationRole>();
            var mockRole = new Mock<IDeletableEntityRepository<ApplicationRole>>();
            mockRole.Setup(x => x.All()).Returns(listRole.AsQueryable());
            mockRole.Setup(x => x.AddAsync(It.IsAny<ApplicationRole>())).Callback(
                (ApplicationRole role) => listRole.Add(role));
            var service = new RoleService(mockRole.Object);
            var role1 = await service.CreateAsyncRole("One");
            var role2 = await service.CreateAsyncRole("Two");
            var role3 = await service.CreateAsyncRole("Three");

            Assert.Equal(3, listRole.Count);
            var result = service.GetById<RoleAllViewModel>(role1);
            Assert.Equal("One", result.Name);
        }
    }
}
