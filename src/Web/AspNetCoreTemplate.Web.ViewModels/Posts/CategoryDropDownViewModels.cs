namespace AspNetCoreTemplate.Web.ViewModels.Posts
{
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class CategoryDropDownViewModels : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
