namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Services.Messaging;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Home;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IUserService userServices;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersDataService usersDataService;
        private readonly ILocationsObjectService locationsObjectService;

        public HomeController(
            ICategoriesService categoriesService,
            IUserService userServices,
            UserManager<ApplicationUser> userManager,
            IUsersDataService usersDataService,
            ILocationsObjectService locationsObjectService)
        {
            this.categoriesService = categoriesService;
            this.userServices = userServices;
            this.userManager = userManager;
            this.usersDataService = usersDataService;
            this.locationsObjectService = locationsObjectService;
        }

        public async Task<IActionResult> Index()
        {
            var userLogIn = await this.userManager.GetUserAsync(this.User);
            if (userLogIn == null)
            {
                var viewModels = new ViewModels.Home.IndexViewModel();

                var categoriess = this.categoriesService.GetAll<IndexCategoryViewModel>();

                viewModels.Categories = categoriess;

                return this.View(viewModels);
            }

            var name = this.usersDataService.GetByUserName<IndexDetailsViewModel>(userLogIn.UserName);
            if (name == null)
            {
                return this.RedirectToAction("Create", "UsersData");
            }

            var locationObj = this.locationsObjectService.GetLocationByUserIdOnly<LocationObjectIndexViewModel>(userLogIn.Id);

            if (locationObj == null)
            {
                return this.RedirectToAction("Create", "LocationsObject");
            }

            var viewModel = new ViewModels.Home.IndexViewModel();

            var categories = this.categoriesService.GetAll<IndexCategoryViewModel>();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            this.ViewData["Message"] = "Email Sent!!!...";
            var viewModel = new EmailModel();
            viewModel.OriginalEmail = GlobalConstants.EmailRegistrationApiKey;
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(EmailModel input)
        {
            this.ViewData["Message"] = "Email Sent!!!...";
            Example emailexample = new Example();
            var email = await this.userServices.CreateAsyncEmail(input);
            input.UserName = input.From;
            var infoBody = GlobalConstants.EmailBodyText + input.From + GlobalConstants.EmailUserName + input.UserName + input.Body;
            var infoBodyCopy = GlobalConstants.EmailBodyTextCopy + input.From + GlobalConstants.EmailUserName + input.UserName + input.Body;
            await emailexample.Execute(input.OriginalEmail, input.To, input.Subject, input.Body, infoBody);
            await emailexample.Execute(input.OriginalEmail, input.From, input.Subject, input.Body, infoBodyCopy);

            return this.RedirectToAction("EmailDetails", "Users", new { id = email });
        }

        public IActionResult Carriers()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
