namespace AspNetCoreTemplate.Web.ViewModels.Users
{
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;

    public class UserIndexView : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
