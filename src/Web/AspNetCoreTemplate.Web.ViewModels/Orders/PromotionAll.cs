namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class PromotionAll
    {
        public IEnumerable<PromotionTypeViewModel> Promotions { get; set; }
    }
}
