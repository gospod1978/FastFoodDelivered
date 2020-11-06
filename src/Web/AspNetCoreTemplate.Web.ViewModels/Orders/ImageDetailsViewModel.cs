namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class ImageDetailsViewModel : IMapFrom<Picture>
    {
        public string Id { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        public string Name { get; set; }

        public string Exstention { get; set; }

        public byte[] DataFiles { get; set; }
    }
}
