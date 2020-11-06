namespace AspNetCoreTemplate.Web.ViewModels.Purchase
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;

    public class CreatePurchaseViewModel
    {
        public string OrderId { get; set; }

        public string OrderName { get; set; }

        public string ResaurantName { get; set; }

        public string LocationRestaurantAreaId { get; set; }

        public string RestaurantId { get; set; }

        public string CourierId { get; set; }

        public string PromotionType { get; set; }

        public decimal MenuPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string LocactionUserId { get; set; }

        public IEnumerable<LocationObjectIndexViewModel> LocationsUser { get; set; }
    }
}
