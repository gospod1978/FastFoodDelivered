namespace AspNetCoreTemplate.Data.Models.Orders
{
    using System;
    using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserDataId { get; set; }

        public string CourierId { get; set; }

        [Required]
        public string RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        [Required]
        public string TimePrepartion { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual PromotionType PromotionType { get; set; }

        public string Picture { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
