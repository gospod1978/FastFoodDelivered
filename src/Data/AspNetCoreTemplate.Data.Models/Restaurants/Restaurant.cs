namespace AspNetCoreTemplate.Data.Models.Restaurants
{
    using System;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;

    public class Restaurant : BaseDeletableModel<string>
    {
        public Restaurant()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Image { get; set; }

        public string Phone { get; set; }

        public string WorkingAreaId { get; set; }

        public virtual WorkingArea WorkingArea { get; set; }

        public virtual IsWorking IsWorking { get; set; }

        public virtual IsRestaurnat IsRestaurnat { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
