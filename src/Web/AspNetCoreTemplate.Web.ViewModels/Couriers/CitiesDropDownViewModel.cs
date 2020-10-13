namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class CitiesDropDownViewModel : IMapFrom<City>
    {
        public string Id { get; set; }

        public string CityName { get; set; }
    }
}
