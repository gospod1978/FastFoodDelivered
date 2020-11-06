namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Services.Mapping;

    public class ChangePromotionViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string PromotionId { get; set; }

        public PromotionType PromotionType { get; set; }
    }
}
