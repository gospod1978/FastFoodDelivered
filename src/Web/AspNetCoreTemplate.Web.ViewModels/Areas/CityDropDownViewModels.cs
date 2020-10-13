namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class CityDropDownViewModels : IMapFrom<City>
    {
        public string Id { get; set; }

        public string CityName { get; set; }
    }
}
