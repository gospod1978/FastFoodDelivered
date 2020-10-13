namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;
    using System.Collections.Generic;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Restaurants;

    public class WorkingArea : BaseDeletableModel<string>
    {
        public WorkingArea()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Couriers = new HashSet<Courier>();
            this.Restaurants = new HashSet<Restaurant>();
            this.Users = new HashSet<ApplicationUser>();
        }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public ActiveWorkingArea ActiveWorkingArea { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public virtual ICollection<Courier> Couriers { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
