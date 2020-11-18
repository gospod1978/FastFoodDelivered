namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class RestaurantAll : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
