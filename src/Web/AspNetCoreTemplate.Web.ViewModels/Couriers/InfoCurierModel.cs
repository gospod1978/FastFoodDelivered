namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class InfoCurierModel : IMapFrom<Courier>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
