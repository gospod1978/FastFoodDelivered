namespace AspNetCoreTemplate.Web.ViewModels.UsersData
{
    using System.ComponentModel.DataAnnotations;

    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;

    public class CreateUserDataViewModel : IMapFrom<UserData>
    {
        [Required]
        [Display(Name="Write your Full Name")]
        public string Name { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string Messages { get; set; }
    }
}
