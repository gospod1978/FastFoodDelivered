using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    public class LocationCreateInputModel
    {
        public string AddressId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
