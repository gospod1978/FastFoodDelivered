namespace AspNetCoreTemplate.Web.ViewModels.LocationObjects
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;

    public class CreateLocationObjectInputModel
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AddressId { get; set; }

        public Address Address { get; set; }
    }
}
