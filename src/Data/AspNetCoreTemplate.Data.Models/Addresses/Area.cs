namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Restaurants;

    public class Area : BaseDeletableModel<string>
    {
        public Area()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Streets = new HashSet<Street>();
            this.Couriers = new HashSet<Courier>();
            this.Restaurants = new HashSet<Restaurant>();
            this.Users = new HashSet<ApplicationUser>();
        }

        [Required]
        public string AreaName { get; set; }

        public string CityId { get; set; }

        public virtual City City { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Street> Streets { get; set; }

        public virtual ICollection<Courier> Couriers { get; set; }

        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
