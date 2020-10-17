namespace AspNetCoreTemplate.Web.ViewModels.Streets
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class StreetByArea : IMapFrom<Area>
    {
        public string Id { get; set; }

        public IEnumerable<AreasDropDownMenu> Areas { get; set; }
    }
}
