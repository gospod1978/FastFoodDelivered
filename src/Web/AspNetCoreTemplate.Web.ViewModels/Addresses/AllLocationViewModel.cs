namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AllLocationViewModel : IMapFrom<LocationObject>
    {
        public string Name { get; set; }

        public string AddressId { get; set; }
    }
}
