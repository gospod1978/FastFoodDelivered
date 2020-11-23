namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Web.ViewModels.Restaurants;

    public class AllUserDataReastaurantViewModel
    {
        public IEnumerable<RestaurantAll> Restaurants { get; set; }
    }
}
