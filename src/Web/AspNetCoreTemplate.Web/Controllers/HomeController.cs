namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Services.Messaging;
    using AspNetCoreTemplate.Web.ViewModels;
    using AspNetCoreTemplate.Web.ViewModels.Home;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IUserService userServices;

        public HomeController(
            ICategoriesService categoriesService,
            IUserService userServices)
        {
            this.categoriesService = categoriesService;
            this.userServices = userServices;
        }

        public IActionResult Index()
        {
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
