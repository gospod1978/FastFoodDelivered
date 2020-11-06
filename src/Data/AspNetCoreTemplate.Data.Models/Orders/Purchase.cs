namespace AspNetCoreTemplate.Data.Models.Orders
{
    using System;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Restaurants;

    public class Purchase : BaseDeletableModel<string>
    {
        public Purchase()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Status Status { get; set; }

        public PromotionType PromotionType { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string CourierId { get; set; }

        public Courier Courier { get; set; }

        public string RestaurantId { get; set; }

        public Restaurant Restaurant { get; set; }

        public decimal Price { get; set; }

        public decimal MenuPrice { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
