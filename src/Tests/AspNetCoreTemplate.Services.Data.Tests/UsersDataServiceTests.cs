namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using Moq;
    using Xunit;

    public class UsersDataServiceTests
    {
        [Fact]
        public async Task CreateUserDataShouldCreateCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listUserData = new List<UserData>();
            var mockUserData = new Mock<IDeletableEntityRepository<UserData>>();
            mockUserData.Setup(x => x.All()).Returns(listUserData.AsQueryable());
            mockUserData.Setup(x => x.AddAsync(It.IsAny<UserData>())).Callback(
                (UserData data) => listUserData.Add(data));
            var service = new UsersDataService(mockUserData.Object);
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "User",
                Email = "user@user.com",
            };
            var userData1 = await service.CreateAsyncUserData("data1", user.Id);
            var userData2 = await service.CreateAsyncUserData("data2", "2");
            var userData3 = await service.CreateAsyncUserData("data3", "3");

            Assert.Equal(3, listUserData.Count);
        }

        [Fact]
        public async Task GetUserDataByIdShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listUserData = new List<UserData>();
            var mockUserData = new Mock<IDeletableEntityRepository<UserData>>();
            mockUserData.Setup(x => x.All()).Returns(listUserData.AsQueryable());
            mockUserData.Setup(x => x.AddAsync(It.IsAny<UserData>())).Callback(
                (UserData data) => listUserData.Add(data));
            var service = new UsersDataService(mockUserData.Object);
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "User",
                Email = "user@user.com",
            };
            var userData1 = await service.CreateAsyncUserData("data1", user.Id);
            var userData2 = await service.CreateAsyncUserData("data2", "2");
            var userData3 = await service.CreateAsyncUserData("data3", "3");

            Assert.Equal(3, listUserData.Count);
            var result = service.GetByUserId<UserDataIndexViewModel>("2");
            Assert.Equal("data2", result.Name);
        }

        [Fact]
        public async Task GetNameUserDataShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listUserData = new List<UserData>();
            var mockUserData = new Mock<IDeletableEntityRepository<UserData>>();
            mockUserData.Setup(x => x.All()).Returns(listUserData.AsQueryable());
            mockUserData.Setup(x => x.AddAsync(It.IsAny<UserData>())).Callback(
                (UserData data) => listUserData.Add(data));
            var service = new UsersDataService(mockUserData.Object);
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "User",
                Email = "user@user.com",
            };
            var userData1 = await service.CreateAsyncUserData("data1", user.Id);
            var userData2 = await service.CreateAsyncUserData("data2", "2");
            var userData3 = await service.CreateAsyncUserData("data3", "3");

            Assert.Equal(3, listUserData.Count);
            var result = service.GetName(user.Id);
            Assert.Equal("data1", result);
        }

        [Fact]
        public async Task GetIdUserDataShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listUserData = new List<UserData>();
            var mockUserData = new Mock<IDeletableEntityRepository<UserData>>();
            mockUserData.Setup(x => x.All()).Returns(listUserData.AsQueryable());
            mockUserData.Setup(x => x.AddAsync(It.IsAny<UserData>())).Callback(
                (UserData data) => listUserData.Add(data));
            var service = new UsersDataService(mockUserData.Object);
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "User",
                Email = "user@user.com",
            };
            var userData1 = await service.CreateAsyncUserData("data1", user.Id);
            var userData2 = await service.CreateAsyncUserData("data2", "2");
            var userData3 = await service.CreateAsyncUserData("data3", "3");

            Assert.Equal(3, listUserData.Count);
            var result = service.GetId(user.Id);
            Assert.Equal(userData1, result);
        }

        [Fact]
        public async Task GetByDataIdUserDataShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listUserData = new List<UserData>();
            var mockUserData = new Mock<IDeletableEntityRepository<UserData>>();
            mockUserData.Setup(x => x.All()).Returns(listUserData.AsQueryable());
            mockUserData.Setup(x => x.AddAsync(It.IsAny<UserData>())).Callback(
                (UserData data) => listUserData.Add(data));
            var service = new UsersDataService(mockUserData.Object);
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "User",
                Email = "user@user.com",
            };
            var userData1 = await service.CreateAsyncUserData("data1", user.Id);
            var userData2 = await service.CreateAsyncUserData("data2", "2");
            var userData3 = await service.CreateAsyncUserData("data3", "3");

            Assert.Equal(3, listUserData.Count);
            var result = service.GetByDataId(userData1);
            Assert.Equal(user.Id, result);
        }

        [Fact]
        public async Task GetByDataIdGUserDataShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listUserData = new List<UserData>();
            var mockUserData = new Mock<IDeletableEntityRepository<UserData>>();
            mockUserData.Setup(x => x.All()).Returns(listUserData.AsQueryable());
            mockUserData.Setup(x => x.AddAsync(It.IsAny<UserData>())).Callback(
                (UserData data) => listUserData.Add(data));
            var service = new UsersDataService(mockUserData.Object);
            var user = new ApplicationUser
            {
                Id = "1",
                UserName = "User",
                Email = "user@user.com",
            };
            var userData1 = await service.CreateAsyncUserData("data1", user.Id);
            var userData2 = await service.CreateAsyncUserData("data2", "2");
            var userData3 = await service.CreateAsyncUserData("data3", "3");

            Assert.Equal(3, listUserData.Count);
            var result = service.GetByDataIdG<UserDataIndexViewModel>(userData1);
            Assert.Equal(user.Id, result.UserId);
        }
    }
}
