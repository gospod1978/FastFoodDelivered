namespace AspNetCoreTemplate.Web.ViewModels.Purchases
{
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Services.Mapping;

    public class TestPurchaseDetails : IMapFrom<Purchase>
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public string UserId { get; set; }

        public string CourierId { get; set; }

        public string RestaurantId { get; set; }

        public string PromotionType { get; set; }

        public decimal MenuPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string Status { get; set; }
    }
}
