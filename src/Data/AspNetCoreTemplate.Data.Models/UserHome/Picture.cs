namespace AspNetCoreTemplate.Data.Models.UserHome
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Orders;

    public class Picture : BaseDeletableModel<string>
    {
        public Picture()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string OrderId { get; set; }

        public Order Order { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Exstention { get; set; }

        [MaxLength]
        public byte[] DataFiles { get; set; }
    }
}
