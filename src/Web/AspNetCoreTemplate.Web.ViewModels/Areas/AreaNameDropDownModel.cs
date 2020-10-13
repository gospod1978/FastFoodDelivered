namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreaNameDropDownModel : IMapFrom<AreaName>
    {
        public string AreaName { get; set; }
    }
}
