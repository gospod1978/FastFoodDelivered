namespace AspNetCoreTemplate.Web.ViewModels.Purchases
{
    using System.Collections.Generic;

    public class AllPurchaseViewModel
    {
        public IEnumerable<DetailsPurchaseViewModel> Purchases { get; set; }
    }
}
