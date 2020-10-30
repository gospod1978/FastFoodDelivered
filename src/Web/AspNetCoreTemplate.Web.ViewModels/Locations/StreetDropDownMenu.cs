namespace AspNetCoreTemplate.Web.ViewModels.Locations
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class StreetDropDownMenu : IMapFrom<Street>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
