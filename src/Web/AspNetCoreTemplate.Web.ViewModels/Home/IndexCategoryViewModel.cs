namespace AspNetCoreTemplate.Web.ViewModels.Home
{
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class IndexCategoryViewModel : IMapFrom<Category>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int OrdersCount { get; set; }

        public string Url => $"/f/{this.Name.Replace(' ', '-')}";
    }
}
