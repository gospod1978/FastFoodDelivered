namespace AspNetCoreTemplate.Data.Models.Couriers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Vehichle : BaseDeletableModel<string>
    {
        public Vehichle()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Couriers = new HashSet<Courier>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Courier> Couriers { get; set; }
    }
}
