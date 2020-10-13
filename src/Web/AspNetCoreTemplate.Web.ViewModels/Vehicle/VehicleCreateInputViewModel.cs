namespace AspNetCoreTemplate.Web.ViewModels.Vehicle
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class VehicleCreateInputViewModel : IMapTo<Vehichle>
    {
        [Required]
        public string Name { get; set; }
    }
}
