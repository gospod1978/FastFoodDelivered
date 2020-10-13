namespace AspNetCoreTemplate.Web.ViewModels.Cities
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class CityCreateInputViewModel : IMapTo<City>
    {
        [Required]
        public string CityName { get; set; }
    }
}
