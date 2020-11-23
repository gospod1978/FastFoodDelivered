namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using System;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class CuriersAll : IMapFrom<Courier>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string VehicleId { get; set; }

        public virtual Vehichle Vehicle { get; set; }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public DateTime Birthday { get; set; }

        public string CityName { get; set; }

        public string UserId { get; set; }
    }
}
