namespace AspNetCoreTemplate.Web.ViewModels.Streets
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreasDropDownMenu : IMapFrom<Area>
    {
        public string Id { get; set; }

        public string AreaName { get; set; }
    }
}
