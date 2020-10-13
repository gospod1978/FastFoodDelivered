namespace AspNetCoreTemplate.Web.ViewModels.Streets
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class StreetDetailsViewModel : IMapFrom<Street>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
