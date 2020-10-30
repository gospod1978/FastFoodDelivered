namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class DetailsAddressIndexViewModel : IMapFrom<Address>
    {
        [Display(Name = "City")]
        public string CityCityName { get; set; }

        [Display(Name = "Area")]
        public string AreaAreaName { get; set; }

        [Display(Name = "Street")]
        public string StreetName { get; set; }

        [Display(Name = "Number")]
        public int LocationNumber { get; set; }

        [Display(Name = "Flour")]
        public int LocationFlour { get; set; }

        [Display(Name = "Entrance")]
        public int LocationEntrance { get; set; }

        [Display(Name = "Apartament")]
        public int LocationApartament { get; set; }
    }
}
