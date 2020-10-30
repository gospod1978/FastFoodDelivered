namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class DocumentDeleted : IMapFrom<Document>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserDataId { get; set; }

        public UserData UserData { get; set; }
    }
}
