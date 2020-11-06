namespace AspNetCoreTemplate.Data.Models.UserHome
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Common.Models;
    using AspNetCoreTemplate.Data.Models.Orders;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Posts = new HashSet<Post>();
            this.Orders = new HashSet<Order>();
        }

        [Required]
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public CategoryType CategoryType { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
