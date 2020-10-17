namespace AspNetCoreTemplate.Web.ViewModels.Streets
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class StreetsAll : IMapFrom<Street>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AreaId { get; set; }

        public virtual Area AreaName { get; set; }

        public int LocatationCount { get; set; }
    }
}
