namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LocationsObjectController : Controller
    {
        private readonly ILocationsObjectService locationsObjectService;
        private readonly IUserService userService;

        public LocationsObjectController(
            ILocationsObjectService locationsObjectService,
            IUserService userService)
        {
            this.locationsObjectService = locationsObjectService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Create(string addressId)
        {
            var userName = this.User.Identity.Name.ToString();
            var userId = this.userService.GetUserName(userName);
            var viewModel = new CreateLocationObjectInputModel();
            viewModel.UserId = userId;
            viewModel.AddressId = addressId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateLocationObjectInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var locobject = await this.locationsObjectService.CreateAsyncLocationObject(input.Name, input.AddressId, input.UserId);
            this.TempData["InfoMessageAreas"] = "Location was created!";
            return this.RedirectToAction(nameof(this.AllByUser));
        }

        [Authorize]
        public IActionResult AllByUser(string id)
        {
            if (id == null)
            {
                var userName = this.User.Identity.Name.ToString();
                id = this.userService.GetUserName(userName);
            }

            var viewModel = new AllLocationObject();
            var locations = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(id);
            viewModel.Locations = locations;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }
    }
}
