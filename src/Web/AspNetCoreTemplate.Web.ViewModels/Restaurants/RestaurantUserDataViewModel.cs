namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class RestaurantUserDataViewModel : IMapFrom<UserData>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
