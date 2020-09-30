namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Vehicle;

    public class CourierAllViewModel
    {
        public IEnumerable<CuriersAll> Curiers { get; set; }

        public IEnumerable<CitiesAll> Cities { get; set; }

        public IEnumerable<VehicleAll> Vehichles { get; set; }
    }
}
