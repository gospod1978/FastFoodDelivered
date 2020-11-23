namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
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
        private readonly IRestaurantService restaurantService;
        private readonly IUsersDataService usersDataService;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IRoleService roleServices,
            IUserService userServices,
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            ICourierService courierService,
            IRestaurantService restaurantService,
            IUsersDataService usersDataService)
        {
            this.userManager = userManager;
            this.roleServices = roleServices;
            this.userServices = userServices;
            this.roleRepository = roleRepository;
            this.courierService = courierService;
            this.restaurantService = restaurantService;
            this.usersDataService = usersDataService;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.userServices.GetByName<UserViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
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
        [Authorize(Roles = "Administrator, Admin")]
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
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult Approve()
        {
            var viewModel = new TakeCourierAll();
            var curiers = this.courierService.GetAllNo<CourierWaitApprove>();
            viewModel.Couriers = curiers;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult ApproveRest()
        {
            var viewModel = new TakeRestaurantAll();
            var restaurants = this.restaurantService.GetAllNo<RestaurantWaitApprove>();
            viewModel.Restaurant = restaurants;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> IsCourier(string id)
        {
            var user = this.userServices.GetById<AplicantUserName>(id);
            var courier = this.courierService.GetByUserId<CourierWaitApprove>(id);
            await this.userServices.AddRoleToUser<string>(user.UserName, GlobalConstants.CourierRoleName);
            this.courierService.MadeIsCourier(courier.Id);

            return this.RedirectToAction(nameof(this.Approve));
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult NoCourier(string id)
        {
            var courier = this.courierService.GetByUserId<CourierWaitApprove>(id);
            this.courierService.MadeIsCourier(courier.Id);

            return this.RedirectToAction(nameof(this.Approve));
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> IsRestaurant(string id)
        {
            var user = this.userServices.GetById<AplicantUserName>(id);
            var restaurant = this.restaurantService.GetByUserId<RestaurantWaitApprove>(id);
            await this.userServices.AddRoleToUser<string>(user.UserName, GlobalConstants.RestaurantRoleName);
            this.restaurantService.MadeIsRestaurant(restaurant.Id);

            return this.RedirectToAction(nameof(this.ApproveRest));
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult NoRestaurant(string id)
        {
            var restaurant = this.restaurantService.GetByUserId<RestaurantWaitApprove>(id);
            this.restaurantService.MadeIsRestaurant(restaurant.Id);

            return this.RedirectToAction(nameof(this.ApproveRest));
        }

        [Authorize]
        [Authorize(Roles = "Admin, Administrator")]
        public IActionResult NavBarAdmin()
        {
            return this.View();
        }

        [Authorize]
        [Authorize(Roles = "Admin, Administrator, Courier, Restaurant")]
        public async Task<IActionResult> SendEmail()
        {
            this.ViewData["Message"] = "Email Sent!!!...";
            var viewModel = new EmailModel();
            var userLogIn = await this.userManager.GetUserAsync(this.User);
            viewModel.From = userLogIn.NormalizedEmail;
            viewModel.UserId = userLogIn.Id;
            viewModel.OriginalEmail = GlobalConstants.EmailRegistrationApiKey;
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [Authorize(Roles = "Admin, Administrator, Courier, Restaurant")]
        public async Task<IActionResult> SendEmail(EmailModel input)
        {
            this.ViewData["Message"] = "Email Sent!!!...";
            Example emailexample = new Example();
            await this.userServices.CreateAsyncEmail(input);
            await emailexample.Execute(input.OriginalEmail, input.To, input.Subject, input.Body, input.Body);

            return this.RedirectToAction(nameof(this.EmailDetails));
        }

        [Authorize]
        [Authorize(Roles = "Admin, Administrator")]
        public IActionResult AllEmail()
        {
            var viewModel = new AllEmail();
            var emails = this.userServices.GetAllEmail<EmailModel>();
            foreach (var email in emails)
            {
                var fullName = this.usersDataService.GetByUserId<UserDataIndexViewModel>(email.UserId);
                if (fullName == null)
                {
                    email.UserName = email.From;
                }
                else
                {
                    email.UserName = fullName.Name;
                }
            }

            viewModel.Emails = emails;
            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Admin, Administrator, Courier, Restaurant")]
        public async Task<IActionResult> EmailById()
        {
            var viewModel = new AllEmail();
            var userLogIn = await this.userManager.GetUserAsync(this.User);
            var emails = this.userServices.GetAllEmailByUserId<EmailModel>(userLogIn.Id);
            foreach (var email in emails)
            {
                var fullName = this.usersDataService.GetByUserId<UserDataIndexViewModel>(email.UserId);
                if (fullName == null)
                {
                    email.UserName = email.From;
                }
                else
                {
                    email.UserName = fullName.Name;
                }
            }

            viewModel.Emails = emails;
            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Admin, Administrator, Courier, Restaurant")]
        public IActionResult EmailDetails(string id)
        {
            var email = this.userServices.GetEmailById<EmailModel>(id);
            var viewModel = new EmailModel();
            viewModel.Id = email.Id;
            viewModel.From = email.From;
            viewModel.To = email.To;
            viewModel.Subject = email.Subject;
            viewModel.Body = email.Body;
            viewModel.CreatedOn = email.CreatedOn;
            viewModel.OriginalEmail = email.OriginalEmail;
            var userName = this.usersDataService.GetByUserId<UserDataIndexViewModel>(email.UserId);
            if (userName == null)
            {
                viewModel.UserName = email.From;
            }
            else
            {
                viewModel.UserName = userName.Name;
            }

            return this.View(viewModel);
        }
    }
}
