namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class RestaurantDetailsViewModel : IMapFrom<Restaurant>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string AreaId { get; set; }

        public string WorkingAreaId { get; set; }

        public string WorkingArea { get; set; }

        public string IsActiv { get; set; }

        public string FullName { get; set; }

        public string City { get; set; }

        public byte[] Photo { get; set; }

        public string ImageName { get; set; }
    }
}
