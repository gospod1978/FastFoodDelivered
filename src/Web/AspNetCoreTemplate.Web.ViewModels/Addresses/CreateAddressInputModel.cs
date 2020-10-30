namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Web.ViewModels.Locations;
    using AspNetCoreTemplate.Web.ViewModels.Streets;

    public class CreateAddressInputModel
    {
        [Display(Name = "City")]
        public string CityId { get; set; }

        public IEnumerable<CitiesDropDownMenuInStreet> Cities { get; set; }

        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public IEnumerable<AreasDropDownMenu> Areas { get; set; }

        [Display(Name = "Street")]
        public string StreetId { get; set; }

        public IEnumerable<StreetDropDownMenu> Streets { get; set; }

        [Display(Name = "Location")]
        public string LocationId { get; set; }

        [Display(Name = "Number")]
        public int Number { get; set; }

        [Display(Name = "Flour")]
        public int Flour { get; set; }

        [Display(Name = "Entrance")]
        public int Entrance { get; set; }

        [Display(Name = "Apartament")]
        public int Apartament { get; set; }

        public string CityName { get; set; }

        public string AreaName { get; set; }

        public string StreetName { get; set; }
    }
}
