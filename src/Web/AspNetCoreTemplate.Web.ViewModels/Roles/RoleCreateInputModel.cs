namespace AspNetCoreTemplate.Web.ViewModels.Roles
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class RoleCreateInputModel : IMapFrom<ApplicationRole>
    {
        [Required]
        public string Name { get; set; }

        public string NormalizedName { get; set; }
    }
}
