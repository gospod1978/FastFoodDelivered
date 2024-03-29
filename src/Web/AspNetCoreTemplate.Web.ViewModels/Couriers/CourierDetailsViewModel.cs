﻿namespace AspNetCoreTemplate.Web.ViewModels.Couriers
{
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class CourierDetailsViewModel : IMapFrom<Courier>
    {
        public string Id { get; set; }

        public string Image { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Vehicle { get; set; }

        public string Birthday { get; set; }

        public string AreaId { get; set; }

        public string WorkingAreaId { get; set; }

        public string WorkingArea { get; set; }

        public string IsActiv { get; set; }

        public string CourierName { get; set; }

        public string City { get; set; }

        public byte[] Photo { get; set; }

        public string ImageName { get; set; }
    }
}
