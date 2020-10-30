namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class UploadDocumentViewModel : IMapFrom<Document>
    {
        public Document Files { get; set; }

        public string UserDataId { get; set; }

        public UserData UserData { get; set; }
    }
}
