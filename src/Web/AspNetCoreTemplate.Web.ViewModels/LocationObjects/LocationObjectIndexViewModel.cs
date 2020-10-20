namespace AspNetCoreTemplate.Web.ViewModels.LocationObjects
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class LocationObjectIndexViewModel : IMapFrom<LocationObject>
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AddressId { get; set; }

        public Address Address { get; set; }

        public virtual AddressInfoIndexViewModel Info { get; set; }
    }
}
