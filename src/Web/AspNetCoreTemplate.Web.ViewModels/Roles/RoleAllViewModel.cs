namespace AspNetCoreTemplate.Web.ViewModels.Roles
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class RoleAllViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
