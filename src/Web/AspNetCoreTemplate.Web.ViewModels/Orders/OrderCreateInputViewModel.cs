namespace AspNetCoreTemplate.Web.ViewModels.Orders
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Http;

    public class OrderCreateInputViewModel
    {
        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }

        public string PhotoId { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string UserDataId { get; set; }

        public string CourierId { get; set; }

        public string RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        [Required]
        public string TimePrepartion { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryDropDownViewModels> Categories { get; set; }
    }
}
