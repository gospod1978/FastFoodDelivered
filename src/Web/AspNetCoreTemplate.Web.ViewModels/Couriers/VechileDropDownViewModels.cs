namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class VechileDropDownViewModels : IMapFrom<Vehichle>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
