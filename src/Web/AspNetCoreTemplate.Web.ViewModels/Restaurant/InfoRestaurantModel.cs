namespace AspNetCoreTemplate.Web.ViewModels.Restaurant
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class InfoRestaurantModel : IMapFrom<Restaurant>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
