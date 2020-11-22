namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using System.Net;
    using System.Text.RegularExpressions;

    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class OrderDetailsViewModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public byte[] Photo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserDataId { get; set; }

        public UserData UserData { get; set; }

        public string CourierId { get; set; }

        public string RestaurantId { get; set; }

        public string RestaurantName { get; set; }

        public string TimePrepartion { get; set; }

        public decimal Price { get; set; }

        public PromotionType PromotionType { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string Content
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 300
                        ? content.Substring(0, 300) + "..."
                        : content;
            }
        }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Description, @"<[^>]+>", string.Empty));
                return content.Length > 5
                        ? content.Substring(0, 5) + "..."
                        : content;
            }
        }
    }
}
