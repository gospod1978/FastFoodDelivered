namespace AspNetCoreTemplate.Web.ViewModels.Vehicle
{
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class VehicleAll : IMapFrom<Vehichle>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
