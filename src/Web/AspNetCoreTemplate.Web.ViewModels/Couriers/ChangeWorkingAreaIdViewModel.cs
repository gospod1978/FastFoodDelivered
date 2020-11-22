namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Web.ViewModels.Streets;

    public class ChangeWorkingAreaIdViewModel
    {
        public string CourierId { get; set; }

        public Courier Courier { get; set; }

        public string RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public string CityName { get; set; }

        public string AreaId { get; set; }

        public IEnumerable<AreasDropDownMenu> Areas { get; set; }
    }
}
