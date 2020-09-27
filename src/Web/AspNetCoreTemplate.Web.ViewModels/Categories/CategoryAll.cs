namespace AspNetCoreTemplate.Web.ViewModels.Categories
{
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class CategoryAll : IMapFrom<Category>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int PostsCount { get; set; }

        public string Url => $"/f/{this.Name.Replace(' ', '-')}";
    }
}
