namespace AspNetCoreTemplate.Web.ViewModels.Locations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Web.ViewModels.Streets;

    public class CreateLocationsInputModel
    {
        public string Id { get; set; }

        public int Number { get; set; }

        public int Flour { get; set; }

        public int Entrance { get; set; }

        public int Apartament { get; set; }

        [Display(Name = "City")]
        public string CityId { get; set; }

        public IEnumerable<CitiesDropDownMenuInStreet> Cities { get; set; }

        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public IEnumerable<AreasDropDownMenu> Areas { get; set; }

        [Display(Name = "Street")]
        public string StreetId { get; set; }

        public IEnumerable<StreetDropDownMenu> Streets { get; set; }

        public string CityName { get; set; }

        public string AreaName { get; set; }

        public string StreetName { get; set; }
    }
}
