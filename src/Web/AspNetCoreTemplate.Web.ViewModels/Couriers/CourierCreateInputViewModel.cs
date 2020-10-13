namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Vehicle;

    public class CourierCreateInputViewModel : IMapTo<Courier>
    {
        public string Image { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Birthday { get; set; }

        [Display(Name = "Vechile")]
        public string VechileId { get; set; }

        public IEnumerable<VehicleAll> Vechiles { get; set; }

        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public IEnumerable<WorkingAllArea> Areas { get; set; }
    }
}
