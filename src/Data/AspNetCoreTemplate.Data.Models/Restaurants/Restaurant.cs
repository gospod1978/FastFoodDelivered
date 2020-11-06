namespace AspNetCoreTemplate.Data.Models.Restaurants
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Orders;

    public class Restaurant : BaseDeletableModel<string>
    {
        public Restaurant()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
        }

        public string Image { get; set; }

        [Required]
        public string Phone { get; set; }

        public string WorkingAreaId { get; set; }

        public virtual WorkingArea WorkingArea { get; set; }

        public virtual IsWorking IsWorking { get; set; }

        public virtual IsRestaurnat IsRestaurnat { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
