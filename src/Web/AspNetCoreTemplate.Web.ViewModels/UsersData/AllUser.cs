namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Web.ViewModels.Users;

    public class AllUser
    {
        public IEnumerable<UserIndexView> Users { get; set; }
    }
}
