namespace AspNetCoreTemplate.Web.ViewModels.Purchases
{
    using System;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Services.Mapping;

    public class DetailsPurchaseViewModel : IMapFrom<Purchase>
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public string OrderName { get; set; }

        public string UserId { get; set; }

        public string RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string CourierId { get; set; }

        public string CourierName { get; set; }

        public string Status { get; set; }

        public string NewStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string PromotionType { get; set; }

        public decimal Price { get; set; }

        public decimal MenuPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public byte[] Photo { get; set; }
    }
}
