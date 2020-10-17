namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreaNameDropDownModel : IMapFrom<Area>
    {
        public string AreaId { get; set; }

        public string AreaName { get; set; }
    }
}
