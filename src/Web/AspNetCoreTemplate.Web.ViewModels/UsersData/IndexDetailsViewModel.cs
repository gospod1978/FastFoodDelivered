namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System.Collections.Generic;

    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class IndexDetailsViewModel : IMapFrom<UserData>
    {
        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public virtual ICollection<DocumentDetails> Documents { get; set; }
    }
}
