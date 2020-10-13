namespace AspNetCoreTemplate.Web.ViewModels.Addresses
{
    using System.Collections.Generic;

    public class LocationIndexViewModel
    {
        public IEnumerable<AllLocationViewModel> Locations { get; set; }
    }
}
