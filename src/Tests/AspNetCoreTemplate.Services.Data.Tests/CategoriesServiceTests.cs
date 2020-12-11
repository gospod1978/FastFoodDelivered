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
    using AspNetCoreTemplate.Web.ViewModels.Categories;
    using Moq;
    using Xunit;

    public class CategoriesServiceTests
    {
        [Fact]
        public async Task CreateCategoryShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCategory = new List<Category>();
            var mockCategory = new Mock<IDeletableEntityRepository<Category>>();
            mockCategory.Setup(x => x.All()).Returns(listCategory.AsQueryable());
            mockCategory.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => listCategory.Add(category));
            var service = new CategoriesService(mockCategory.Object);
            var category1 = await service.CreateAsync("One", "One", "One", "One");
            var category2 = await service.CreateAsync("Two", "Two", "Two", "Two");
            var category3 = await service.CreateAsync("Three", "Three", "Three", "Three");

            Assert.Equal(3, listCategory.Count);
        }

        [Fact]
        public async Task GetAllCategoryShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCategory = new List<Category>();
            var mockCategory = new Mock<IDeletableEntityRepository<Category>>();
            mockCategory.Setup(x => x.All()).Returns(listCategory.AsQueryable());
            mockCategory.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => listCategory.Add(category));
            var service = new CategoriesService(mockCategory.Object);
            var category1 = await service.CreateAsync("One", "One", "One", "One");
            var category2 = await service.CreateAsync("Two", "Two", "Two", "Two");
            var category3 = await service.CreateAsync("Three", "Three", "Three", "Three");

            Assert.Equal(3, listCategory.Count);
            var result = service.GetAll<CategoryAll>();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public async Task GetByIdCategoryShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCategory = new List<Category>();
            var mockCategory = new Mock<IDeletableEntityRepository<Category>>();
            mockCategory.Setup(x => x.All()).Returns(listCategory.AsQueryable());
            mockCategory.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => listCategory.Add(category));
            var service = new CategoriesService(mockCategory.Object);
            var category1 = await service.CreateAsync("One", "One", "One", "One");
            var category2 = await service.CreateAsync("Two", "Two", "Two", "Two");
            var category3 = await service.CreateAsync("Three", "Three", "Three", "Three");

            Assert.Equal(3, listCategory.Count);
            var result = service.GetById<CategoryAll>(category1);
            Assert.Equal("One", result.Name);
        }

        [Fact]
        public async Task GetByNameCategoryShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listCategory = new List<Category>();
            var mockCategory = new Mock<IDeletableEntityRepository<Category>>();
            mockCategory.Setup(x => x.All()).Returns(listCategory.AsQueryable());
            mockCategory.Setup(x => x.AddAsync(It.IsAny<Category>())).Callback(
                (Category category) => listCategory.Add(category));
            var service = new CategoriesService(mockCategory.Object);
            var category1 = await service.CreateAsync("One", "One", "One", "One");
            var category2 = await service.CreateAsync("Two", "Two", "Two", "Two");
            var category3 = await service.CreateAsync("Three", "Three", "Three", "Three");

            Assert.Equal(3, listCategory.Count);
            var result = service.GetByName<CategoryAll>("Two");
            Assert.Equal("Two", result.Description);
        }
    }
}
