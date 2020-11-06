namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Vehicle;
    using Microsoft.AspNetCore.Http;

    public class CourierCreateInputViewModel : IMapFrom<Courier>
    {
        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Display(Name = "Vechile")]
        public string VechileId { get; set; }

        public IEnumerable<VehicleAll> Vechiles { get; set; }

        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public IEnumerable<AreasAll> Areas { get; set; }

        public string PhotoId { get; set; }

        public byte[] Photo { get; set; }
    }
}
