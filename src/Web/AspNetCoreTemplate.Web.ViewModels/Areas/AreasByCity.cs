namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreasByCity : IMapFrom<City>
    {
        public string Id { get; set; }

        public IEnumerable<CityDropDownViewModels> Cities { get; set; }
    }
}
