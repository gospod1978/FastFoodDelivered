namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.Roles;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleServices;
        private readonly IUserService userServices;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;
        private readonly ICourierService courierService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IRoleService roleServices,
            IUserService userServices,
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            ICourierService courierService)
        {
            this.userManager = userManager;
            this.roleServices = roleServices;
            this.userServices = userServices;
            this.roleRepository = roleRepository;
            this.courierService = courierService;
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
        public IActionResult Approve()
        {
            var viewModel = new TakeCourierAll();
            var curiers = this.courierService.GetAllNo<CourierWaitApprove>();
            viewModel.Couriers = curiers;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult IsCourier(string id)
        {
            var user = this.userServices.GetById<CurierUserName>(id);
            var courier = this.courierService.GetByUserId<CourierWaitApprove>(id);
            this.courierService.MadeIsCourier(courier.Id);

            this.userServices.AddRoleToUser<string>(user.UserName, "Courier");
            return this.RedirectToAction(nameof(this.Approve));
        }

        [Authorize]
        public IActionResult NoCourier(string id)
        {
            var courier = this.courierService.GetByUserId<CourierWaitApprove>(id);
            this.courierService.MadeIsCourier(courier.Id);

            return this.RedirectToAction(nameof(this.Approve));
        }

        [Authorize]
        public IActionResult NavBarAdmin()
        {
            return this.View();
        }
    }
}
