namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    using System.ComponentModel.DataAnnotations;

    public class AddressCreateInputModel
    {
        [Required]
        public string CityName { get; set; }

        [Required]
        public string AreaName { get; set; }

        [Required]
        public string StreetName { get; set; }

        [Required]
        public int Number { get; set; }

        public int Flour { get; set; }

        public int Entrance { get; set; }

        public int Apartament { get; set; }
    }
}
