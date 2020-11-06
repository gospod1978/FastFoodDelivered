namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class RestaurantWaitApprove : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual IsRestaurnat IsRestaurnat { get; set; }
    }
}
