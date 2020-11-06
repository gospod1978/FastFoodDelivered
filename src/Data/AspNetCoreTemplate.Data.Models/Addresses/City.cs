namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;

    public class City : BaseDeletableModel<string>
    {
        public City()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Areas = new HashSet<Area>();
        }

        [Required]
        public string CityName { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
    }
}
