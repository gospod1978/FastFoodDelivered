namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class RestaurantModelTest : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string Image { get; set; }

        public string AreaId { get; set; }

        public string UserId { get; set; }

        public string WorkingArea { get; set; }
    }
}
