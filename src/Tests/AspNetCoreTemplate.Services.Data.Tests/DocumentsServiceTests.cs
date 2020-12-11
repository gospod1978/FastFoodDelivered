namespace AspNetCoreTemplate.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using Moq;
    using Xunit;

    public class DocumentsServiceTests
    {
        [Fact]
        public async Task CreateDocumentShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listDocument = new List<Document>();
            var mockDocument = new Mock<IDeletableEntityRepository<Document>>();
            mockDocument.Setup(x => x.All()).Returns(listDocument.AsQueryable());
            mockDocument.Setup(x => x.AddAsync(It.IsAny<Document>())).Callback(
                (Document document) => listDocument.Add(document));
            var service = new DocumentsService(mockDocument.Object);
            var userData = new UserData
            {
                Id = "1",
                UserId = "1",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncDocument("One", "jpg", arr1, userData.Id);
            var document2 = await service.CreateAsyncDocument("Two", "png", arr2, userData.Id);
            var document3 = await service.CreateAsyncDocument("Three", "jpeg", arr3, userData.Id);

            Assert.Equal(3, listDocument.Count);
        }

        [Fact]
        public async Task GetAllDocumentsShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listDocument = new List<Document>();
            var mockDocument = new Mock<IDeletableEntityRepository<Document>>();
            mockDocument.Setup(x => x.All()).Returns(listDocument.AsQueryable());
            mockDocument.Setup(x => x.AddAsync(It.IsAny<Document>())).Callback(
                (Document document) => listDocument.Add(document));
            var service = new DocumentsService(mockDocument.Object);
            var userData = new UserData
            {
                Id = "1",
                UserId = "1",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncDocument("One", "jpg", arr1, userData.Id);
            var document2 = await service.CreateAsyncDocument("Two", "png", arr2, userData.Id);
            var document3 = await service.CreateAsyncDocument("Three", "jpeg", arr3, userData.Id);

            Assert.Equal(3, listDocument.Count);
            var result = service.GetAllDocuments<DocumentDetails>();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetAllByUserIdDocumentsShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listDocument = new List<Document>();
            var mockDocument = new Mock<IDeletableEntityRepository<Document>>();
            mockDocument.Setup(x => x.All()).Returns(listDocument.AsQueryable());
            mockDocument.Setup(x => x.AddAsync(It.IsAny<Document>())).Callback(
                (Document document) => listDocument.Add(document));
            var service = new DocumentsService(mockDocument.Object);
            var userData = new UserData
            {
                Id = "1",
                UserId = "1",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncDocument("One", "jpg", arr1, userData.Id);
            var document2 = await service.CreateAsyncDocument("Two", "png", arr2, userData.Id);
            var document3 = await service.CreateAsyncDocument("Three", "jpeg", arr3, "2");

            Assert.Equal(3, listDocument.Count);
            var result = service.GetAllDocumentsByUser<DocumentDetails>(userData.Id);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllByIdDocumentsShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listDocument = new List<Document>();
            var mockDocument = new Mock<IDeletableEntityRepository<Document>>();
            mockDocument.Setup(x => x.All()).Returns(listDocument.AsQueryable());
            mockDocument.Setup(x => x.AddAsync(It.IsAny<Document>())).Callback(
                (Document document) => listDocument.Add(document));
            var service = new DocumentsService(mockDocument.Object);
            var userData = new UserData
            {
                Id = "1",
                UserId = "1",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncDocument("One", "jpg", arr1, userData.Id);
            var document2 = await service.CreateAsyncDocument("Two", "png", arr2, userData.Id);
            var document3 = await service.CreateAsyncDocument("Three", "jpeg", arr3, userData.Id);

            Assert.Equal(3, listDocument.Count);
            var result = service.GetById<DocumentDetails>(document1);
            Assert.Equal("jpg", result.FileType);
        }

        [Fact]
        public async Task GetAllByNameDocumentsShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listDocument = new List<Document>();
            var mockDocument = new Mock<IDeletableEntityRepository<Document>>();
            mockDocument.Setup(x => x.All()).Returns(listDocument.AsQueryable());
            mockDocument.Setup(x => x.AddAsync(It.IsAny<Document>())).Callback(
                (Document document) => listDocument.Add(document));
            var service = new DocumentsService(mockDocument.Object);
            var userData = new UserData
            {
                Id = "1",
                UserId = "1",
            };
            var arr1 = new byte[100];
            var arr2 = new byte[200];
            var arr3 = new byte[300];
            var document1 = await service.CreateAsyncDocument("One", "jpg", arr1, userData.Id);
            var document2 = await service.CreateAsyncDocument("Two", "png", arr2, userData.Id);
            var document3 = await service.CreateAsyncDocument("Three", "jpeg", arr3, userData.Id);

            Assert.Equal(3, listDocument.Count);
            var result = service.GetByName<DocumentDetails>("One");
            Assert.Equal("jpg", result.FileType);
        }
    }
}
