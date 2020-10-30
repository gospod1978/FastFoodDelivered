namespace AspNetCoreTemplate.Data.Models.Orders
{
    using System;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Data.Models.UserHome;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string CourierId { get; set; }

        public virtual Courier Courier { get; set; }

        public string RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public string TimePrepartion { get; set; }

        public string TimeDelivery { get; set; }

        public decimal Price { get; set; }

        public decimal MenuPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public string PromotionType { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
