namespace AspNetCoreTemplate.Web.ViewModels.Restaurants
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using Microsoft.AspNetCore.Http;

    public class RestaurantCreateInputViewModel : IMapFrom<Restaurant>
    {
        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }

        [Required]
        public string Phone { get; set; }

        [Display(Name = "Area")]
        public string AreaId { get; set; }

        public IEnumerable<AreasAll> Areas { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string PhotoId { get; set; }

        public byte[] Photo { get; set; }
    }
}
