namespace AspNetCoreTemplate.Web.ViewModels.Cities
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class CitiesAll : IMapFrom<City>
    {
        public string Id { get; set; }

        public string CityName { get; set; }

        public int AreasCount { get; set; }

        public string Url => $"/a/{this.CityName.ToString().Replace(' ', '-')}";
    }
}
