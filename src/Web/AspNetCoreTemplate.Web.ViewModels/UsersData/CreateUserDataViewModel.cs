namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class CreateUserDataViewModel : IMapFrom<UserData>
    {
        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }
    }
}
