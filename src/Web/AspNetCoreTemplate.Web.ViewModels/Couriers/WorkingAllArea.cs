namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class WorkingAllArea : IMapFrom<WorkingArea>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
