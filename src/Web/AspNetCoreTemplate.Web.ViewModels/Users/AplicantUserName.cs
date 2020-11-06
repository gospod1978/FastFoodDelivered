namespace AspNetCoreTemplate.Web.ViewModels.Users
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class AplicantUserName : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
    }
}
