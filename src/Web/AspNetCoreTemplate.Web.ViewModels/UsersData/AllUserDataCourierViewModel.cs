namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Web.ViewModels.Couriers;

    public class AllUserDataCourierViewModel
    {
        public IEnumerable<CuriersAll> Couriers { get; set; }
    }
}
