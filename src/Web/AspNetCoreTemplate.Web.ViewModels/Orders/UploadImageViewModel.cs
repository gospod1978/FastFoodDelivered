namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class UploadImageViewModel : IMapFrom<Picture>
    {
        public IFormFile Files { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}
