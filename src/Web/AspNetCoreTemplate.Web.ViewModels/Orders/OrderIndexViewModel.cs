namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using System.Net;
    using System.Text.RegularExpressions;

    public class OrderIndexViewModel
    {
        public string OrderId { get; set; }

        public byte[] Photo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TimePrepartion { get; set; }

        public decimal Price { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string RestaurantName { get; set; }

        public string CategoryName { get; set; }

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
    }
}
