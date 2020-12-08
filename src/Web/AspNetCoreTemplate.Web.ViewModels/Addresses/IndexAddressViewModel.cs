namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class IndexAddressViewModel : IMapFrom<Address>
    {
        public string Id { get; set; }

        public string CityId { get; set; }

        public string AreaId { get; set; }

        public string StreetId { get; set; }

        public string LocationId { get; set; }
    }
}
