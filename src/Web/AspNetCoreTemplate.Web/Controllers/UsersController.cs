namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
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
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IRoleServices roleServices,
            IUserServices userServices,
            IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.userManager = userManager;
            this.roleServices = roleServices;
            this.userServices = userServices;
            this.roleRepository = roleRepository;
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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var role = this.roleServices.GetById<string>(input.RoleId);

            await this.userServices.AddRoleToUser<string>(input.UserName, role);
            this.TempData["InfoMessageAddRole"] = $"Role {role} added to {input.UserName}!";
            return this.RedirectToAction(nameof(this.Create));
        }

        [Authorize]
        public IActionResult NavBarAdmin()
        {
            return this.View();
        }
    }
}
