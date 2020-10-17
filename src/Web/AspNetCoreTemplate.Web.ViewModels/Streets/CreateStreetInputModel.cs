namespace AspNetCoreTemplate.Web.ViewModels.Streets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateStreetInputModel
    {
        public string Name { get; set; }

        [Display(Name = "City")]
        public string CityId { get; set; }

        public IEnumerable<CitiesDropDownMenuInStreet> Cities { get; set; }

        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public IEnumerable<AreasDropDownMenu> Areas { get; set; }

        public string CityName { get; set; }

        public string AreaName { get; set; }
    }
}
