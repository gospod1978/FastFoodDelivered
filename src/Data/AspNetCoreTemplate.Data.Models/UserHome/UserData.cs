namespace AspNetCoreTemplate.Data.Models.UserHome
{
    using System;
    using System.Collections.Generic;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Orders;

    public class UserData : BaseDeletableModel<string>
    {
        public UserData()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Documents = new HashSet<Document>();
            this.Photos = new HashSet<Photo>();
            this.Orders = new HashSet<Order>();
        }

        public string Name { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
