namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Services.Mapping;

    public class PromotionTypeViewModel : IMapFrom<PromotionType>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
