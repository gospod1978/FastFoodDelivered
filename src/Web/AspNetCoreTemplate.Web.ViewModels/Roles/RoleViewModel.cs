namespace AspNetCoreTemplate.Web.ViewModels.Roles
{
    using System.Collections.Generic;

    public class RoleViewModel
    {
        public IEnumerable<RoleAllViewModel> Roles { get; set; }
    }
}
