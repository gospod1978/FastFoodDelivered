namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreasByCity : IMapFrom<City>
    {
        public string Id { get; set; }
    }
}
