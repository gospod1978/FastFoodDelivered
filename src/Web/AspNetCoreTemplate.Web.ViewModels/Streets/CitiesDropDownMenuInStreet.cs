namespace AspNetCoreTemplate.Web.ViewModels.Streets
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class CitiesDropDownMenuInStreet : IMapFrom<City>
    {
        public string Id { get; set; }

        public string CityName { get; set; }
    }
}
