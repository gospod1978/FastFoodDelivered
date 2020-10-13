namespace AspNetCoreTemplate.Data.Models.Addresses
{
    using System;

    using AspNetCoreTemplate.Data.Common.Models;

    public class LocationObject : BaseDeletableModel<string>
    {
        public LocationObject()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string AddressId { get; set; }

        public virtual Address Address { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
