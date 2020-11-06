namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderDetailsViewModel> Orders { get; set; }
    }
}
