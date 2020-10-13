namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;
    using System.Collections.Generic;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Street : BaseDeletableModel<string>
    {
        public Street()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Locations = new HashSet<Location>();
        }

        public string Name { get; set; }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
    }
}
