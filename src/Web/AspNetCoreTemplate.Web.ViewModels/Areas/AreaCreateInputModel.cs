namespace AspNetCoreTemplate.Web.ViewModels.Areas
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreaCreateInputModel : IMapTo<Area>
    {
        [Display(Name = "City")]
        public string CityId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string AreaName { get; set; }

        public IEnumerable<CityDropDownViewModels> Cities { get; set; }

        public IEnumerable<AreaNameDropDownModel> AreasName { get; set; }

        public string Image { get; set; }
    }
}
