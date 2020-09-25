namespace AspNetCoreTemplate.Web.ViewModels.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using AutoMapper;

    public class UserAddRoleCreateInputModel : IMapTo<ApplicationUser>, IHaveCustomMappings
    {
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public string RoleId { get; set; }

        public IEnumerable<RolesDropDownViewModels> Roles { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationRole, UserAddRoleCreateInputModel>()
                .ForMember(x => x.RoleId, options =>
                {
                    options.MapFrom(p => p.Id);
                });
        }
    }
}
