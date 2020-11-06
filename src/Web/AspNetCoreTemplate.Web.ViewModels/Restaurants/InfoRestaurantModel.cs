namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class InfoRestaurantModel : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
