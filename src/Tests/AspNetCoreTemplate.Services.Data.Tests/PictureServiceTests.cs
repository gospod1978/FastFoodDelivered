namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Orders;
    using Moq;
    using Xunit;

    public class PictureServiceTests
    {
        [Fact]
        public async Task CreateImageShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPhoto = new List<Picture>();
            var mockPhoto = new Mock<IDeletableEntityRepository<Picture>>();
            mockPhoto.Setup(x => x.All()).Returns(listPhoto.AsQueryable());
            mockPhoto.Setup(x => x.AddAsync(It.IsAny<Picture>())).Callback(
                (Picture photo) => listPhoto.Add(photo));
            var service = new PictureService(mockPhoto.Object);
            var order = new Order
            {
                Id = "1",
                Name = "Order",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncImage("One", "jpg", arr1, order.Id);
            var document2 = await service.CreateAsyncImage("Two", "png", arr2, order.Id);
            var document3 = await service.CreateAsyncImage("Three", "jpeg", arr3, order.Id);

            Assert.Equal(3, listPhoto.Count);
        }

        [Fact]
        public async Task GetAllImagesShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPhoto = new List<Picture>();
            var mockPhoto = new Mock<IDeletableEntityRepository<Picture>>();
            mockPhoto.Setup(x => x.All()).Returns(listPhoto.AsQueryable());
            mockPhoto.Setup(x => x.AddAsync(It.IsAny<Picture>())).Callback(
                (Picture photo) => listPhoto.Add(photo));
            var service = new PictureService(mockPhoto.Object);
            var order = new Order
            {
                Id = "1",
                Name = "Order",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncImage("One", "jpg", arr1, order.Id);
            var document2 = await service.CreateAsyncImage("Two", "png", arr2, order.Id);
            var document3 = await service.CreateAsyncImage("Three", "jpeg", arr3, order.Id);

            Assert.Equal(3, listPhoto.Count);
            var result = service.GetAllImages<ImageDetailsViewModel>();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllImagesByOrderIdShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPhoto = new List<Picture>();
            var mockPhoto = new Mock<IDeletableEntityRepository<Picture>>();
            mockPhoto.Setup(x => x.All()).Returns(listPhoto.AsQueryable());
            mockPhoto.Setup(x => x.AddAsync(It.IsAny<Picture>())).Callback(
                (Picture photo) => listPhoto.Add(photo));
            var service = new PictureService(mockPhoto.Object);
            var order = new Order
            {
                Id = "1",
                Name = "Order",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncImage("One", "jpg", arr1, order.Id);
            var document2 = await service.CreateAsyncImage("Two", "png", arr2, order.Id);
            var document3 = await service.CreateAsyncImage("Three", "jpeg", arr3, "2");

            Assert.Equal(3, listPhoto.Count);
            var result = service.GetAllImagesByOrderId<ImageDetailsViewModel>(order.Id);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetImagesByIdShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPhoto = new List<Picture>();
            var mockPhoto = new Mock<IDeletableEntityRepository<Picture>>();
            mockPhoto.Setup(x => x.All()).Returns(listPhoto.AsQueryable());
            mockPhoto.Setup(x => x.AddAsync(It.IsAny<Picture>())).Callback(
                (Picture photo) => listPhoto.Add(photo));
            var service = new PictureService(mockPhoto.Object);
            var order = new Order
            {
                Id = "1",
                Name = "Order",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncImage("One", "jpg", arr1, order.Id);
            var document2 = await service.CreateAsyncImage("Two", "png", arr2, order.Id);
            var document3 = await service.CreateAsyncImage("Three", "jpeg", arr3, "2");

            Assert.Equal(3, listPhoto.Count);
            var result = service.GetById<ImageDetailsViewModel>(document1);
            Assert.Equal("jpg", result.Exstention);
        }

        [Fact]
        public async Task GetImagesByNameShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPhoto = new List<Picture>();
            var mockPhoto = new Mock<IDeletableEntityRepository<Picture>>();
            mockPhoto.Setup(x => x.All()).Returns(listPhoto.AsQueryable());
            mockPhoto.Setup(x => x.AddAsync(It.IsAny<Picture>())).Callback(
                (Picture photo) => listPhoto.Add(photo));
            var service = new PictureService(mockPhoto.Object);
            var order = new Order
            {
                Id = "1",
                Name = "Order",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncImage("One", "jpg", arr1, order.Id);
            var document2 = await service.CreateAsyncImage("Two", "png", arr2, order.Id);
            var document3 = await service.CreateAsyncImage("Three", "jpeg", arr3, "2");

            Assert.Equal(3, listPhoto.Count);
            var result = service.GetByName<ImageDetailsViewModel>("One");
            Assert.Equal("jpg", result.Exstention);
        }
    }
}
