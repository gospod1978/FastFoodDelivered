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
    using AspNetCoreTemplate.Web.ViewModels.Posts;
    using Moq;
    using Xunit;

    public class PostsServiceTests
    {
        [Fact]
        public async Task CreatePostShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPost = new List<Post>();
            var mockPost = new Mock<IDeletableEntityRepository<Post>>();
            mockPost.Setup(x => x.All()).Returns(listPost.AsQueryable());
            mockPost.Setup(x => x.AddAsync(It.IsAny<Post>())).Callback(
                (Post photo) => listPost.Add(photo));
            var service = new PostsService(mockPost.Object);

            var post1 = await service.CreateAsync("Title1", "Contentent1", 1, "11");
            var post2 = await service.CreateAsync("Title2", "Contentent2", 1, "12");
            var post3 = await service.CreateAsync("Title3", "Contentent3", 2, "11");

            Assert.Equal(3, listPost.Count);
        }

        [Fact]
        public async Task GetCountByCategoryIdShouldReturnCorrectInfo()
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            var listPost = new List<Post>();
            var mockPost = new Mock<IDeletableEntityRepository<Post>>();
            mockPost.Setup(x => x.All()).Returns(listPost.AsQueryable());
            mockPost.Setup(x => x.AddAsync(It.IsAny<Post>())).Callback(
                (Post photo) => listPost.Add(photo));
            var service = new PostsService(mockPost.Object);

            var post1 = await service.CreateAsync("Title1", "Contentent1", 1, "11");
            var post2 = await service.CreateAsync("Title2", "Contentent2", 1, "12");
            var post3 = await service.CreateAsync("Title3", "Contentent3", 2, "11");

            Assert.Equal(3, listPost.Count);
            var result = service.GetCountByCategoryId(1);
            Assert.Equal(2, result);
        }
    }
}
