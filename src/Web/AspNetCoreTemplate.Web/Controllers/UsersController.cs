namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleServices roleServices;
        private readonly IUserServices userServices;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IRoleServices roleServices,
            IUserServices userServices)
        {
            this.userManager = userManager;
            this.roleServices = roleServices;
            this.userServices = userServices;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.userServices.GetByName<UserViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var roles = this.roleServices.GetAll<RolesDropDownViewModels>();
            var viewModel = new UserAddRoleCreateInputModel()
            {
                Roles = roles,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(UserAddRoleCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.userServices.AddRoleToUser<string>(input.UserName, input.RoleId);
            var role = this.userServices.GetById<string>(input.RoleId);
            this.TempData["InfoMessage"] = $"Role {role} added to {input.UserName}!";
            return this.RedirectToAction(nameof(this.Create));
        }
    }
}
