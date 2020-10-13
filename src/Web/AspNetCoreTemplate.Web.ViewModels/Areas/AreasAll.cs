namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreasAll : IMapFrom<Area>
    {
        public string Id { get; set; }

        public AreaName AreaName { get; set; }

        public string CityId { get; set; }

        public virtual City City { get; set; }

        public string Image { get; set; }

        public string Url => $"/a/{this.AreaName.ToString().Replace(' ', '-')}";
    }
}
