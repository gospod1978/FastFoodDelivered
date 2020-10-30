namespace AspNetCoreTemplate.Data.Models.Couriers
{
    using System;
    using System.Collections.Generic;
    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Orders;

    public class Courier : BaseDeletableModel<string>
    {
        public Courier()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
        }

        public string Image { get; set; }

        public string Phone { get; set; }

        public string VehicleId { get; set; }

        public virtual Vehichle Vehicle { get; set; }

        public DateTime Birthday { get; set; }

        public string WorkingAreaId { get; set; }

        public virtual WorkingArea WorkingArea { get; set; }

        public virtual IsWorking IsWorking { get; set; }

        public virtual IsCourier IsCourier { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
