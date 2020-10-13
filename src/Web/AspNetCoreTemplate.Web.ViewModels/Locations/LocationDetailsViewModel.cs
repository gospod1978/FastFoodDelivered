namespace AspNetCoreTemplate.Web.ViewModels.Locations
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class LocationDetailsViewModel : IMapFrom<Location>
    {
        public string Id { get; set; }

        public int Number { get; set; }

        public int Flour { get; set; }

        public int Entrance { get; set; }

        public int Apartament { get; set; }
    }
}
