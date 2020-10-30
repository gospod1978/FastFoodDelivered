namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class UploadImageViewModel : IMapFrom<Photo>
    {
        public IFormFile Files { get; set; }

        public string UserDataId { get; set; }

        public UserData UserData { get; set; }
    }
}
