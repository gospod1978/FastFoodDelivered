namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Location : BaseDeletableModel<string>
    {
        public Location()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public int Number { get; set; }

        public int Flour { get; set; }

        public int Entrance { get; set; }

        public int Apartament { get; set; }

        public string StreetId { get; set; }

        public virtual Street Street { get; set; }
    }
}
