namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using System;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class CourierDetailsViewModel : IMapFrom<Courier>
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public Vehichle Vehicle { get; set; }

        public City City { get; set; }

        public DateTime Birthday { get; set; }
    }
}
