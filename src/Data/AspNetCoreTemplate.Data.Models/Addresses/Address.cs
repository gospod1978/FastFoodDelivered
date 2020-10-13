namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Address : BaseDeletableModel<string>
    {
        public Address()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string CityId { get; set; }

        public virtual City City { get; set; }

        public string AreaId { get; set; }

        public virtual Area Area { get; set; }

        public string StreetId { get; set; }

        public virtual Street Street { get; set; }

        public string LocationId { get; set; }

        public virtual Location Location { get; set; }
    }
}
