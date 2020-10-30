﻿namespace AspNetCoreTemplate.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Restaurant;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RestaurantController : Controller
    {
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICitiesService citiesService;
        private readonly ILocationsObjectService locationsObjectService;
        private readonly IAreasService areasService;
        private readonly IUsersDataService usersDataService;
        private readonly IUserService userService;
        private readonly IRestaurantService restaurantService;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IImagesService imagesService;

        public RestaurantController(
            IAddressService addressService,
            UserManager<ApplicationUser> userManager,
            ICitiesService citiesService,
            ILocationsObjectService locationsObjectService,
            IAreasService areasService,
            IUsersDataService usersDataService,
            IUserService userService,
            IRestaurantService restaurantService,
            IWebHostEnvironment hostEnvironment,
            IImagesService imagesService)
        {
            this.addressService = addressService;
            this.userManager = userManager;
            this.citiesService = citiesService;
            this.locationsObjectService = locationsObjectService;
            this.areasService = areasService;
            this.usersDataService = usersDataService;
            this.userService = userService;
            this.restaurantService = restaurantService;
            this.hostEnvironment = hostEnvironment;
            this.imagesService = imagesService;
        }

        [Authorize]
        public async Task<IActionResult> Create1()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var location = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(user.Id).ToList();
            if (location.Count() == 0)
            {
                return this.RedirectToAction("Create", "Address");
            }

            var cityId = location.Where(x => x.UserId == user.Id).Select(c => c.Address.CityId).FirstOrDefault();
            var city = this.citiesService.GetById<CitiesAll>(cityId);
            var cityName = city.CityName;

            return location.Count == 0 ? this.RedirectToAction("Create", "Address") : this.RedirectToAction(nameof(this.Create), new { name = cityName, id = city.Id });
        }

        [Authorize]
        public IActionResult Create2(string id)
        {
            var viewModel = new RestaurantCreateInputViewModel();

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create(string id)
        {
            var areas = this.areasService.GetAllAreas<AreasAll>(id);
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = new RestaurantCreateInputViewModel();

            viewModel.Areas = areas;
            viewModel.UserId = user.Id;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create3(string id, string userId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var location = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(user.Id).ToList();
            var cityId = location.Where(x => x.UserId == user.Id).Select(c => c.Address.CityId).FirstOrDefault();
            var areas = this.areasService.GetAllAreas<AreasAll>(cityId);
            var image = this.imagesService.GetById<ImageDetailsViewModel>(id);

            var viewModel = new RestaurantCreateInputViewModel();
            var stream = new MemoryStream(image.DataFiles);
            viewModel.Areas = areas;
            viewModel.Photo = image.DataFiles;
            viewModel.ImageName = image.Name;
            viewModel.PhotoId = id;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RestaurantCreateInputViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string image = string.Empty;

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var workingArea = this.addressService.CreateAsyncWorkingArea(input.AreaId, user.Id);

            var restaurantId = await this.restaurantService.CreateAsync(input.ImageName, input.Phone, workingArea.Result, user.Id, input.AreaId);
            this.TempData["InfoMessageCourier"] = "You applied for Restaurant!";
            return this.RedirectToAction(nameof(this.Details), new { id = restaurantId });
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                var userLogin = await this.userManager.GetUserAsync(this.User);
                var restaurant = this.restaurantService.GetByUserId<RestaurantAll>(userLogin.Id);
                id = restaurant.Id;
            }

            var restaurants = this.restaurantService.GetById<RestaurantAll>(id);
            var restaurantUser = this.restaurantService.GetById<InfoRestaurantModel>(id);
            var user = this.usersDataService.GetByUserId<RestaurantUserDataViewModel>(restaurantUser.UserId);
            var userData = this.userService.GetById<UserViewCourier>(restaurantUser.UserId);
            var workingArea = this.addressService.GetByWorkingAreaByUserId(restaurantUser.UserId);
            var area = this.areasService.GetById<AreasAll>(workingArea.AreaId);
            var photo = this.imagesService.GetAllImagesByUser<ImageDetailsViewModel>(user.Id).FirstOrDefault();
            var viewModel = new RestaurantDetailsViewModel();

            viewModel.CourierName = user.Name;
            viewModel.City = area.City.CityName;
            viewModel.Email = userData.Email;
            viewModel.Photo = photo.DataFiles;
            viewModel.ImageName = photo.Name;
            viewModel.Id = id;
            viewModel.Phone = restaurants.Phone;
            viewModel.WorkingArea = area.AreaName;
            viewModel.IsActiv = workingArea.ActiveWorkingArea.ToString();
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        private string Image()
        {
            string imageUrl;
            System.Random random = new System.Random();
            int number = random.Next(1, 6);

            switch (number)
            {
                case 1:
                    imageUrl = AllImage.DriverOne;
                    break;
                case 2:
                    imageUrl = AllImage.DriverTwo;
                    break;
                case 3:
                    imageUrl = AllImage.DriverThree;
                    break;
                case 4:
                    imageUrl = AllImage.DriverFour;
                    break;
                case 5:
                    imageUrl = AllImage.DriverFive;
                    break;
                case 6:
                    imageUrl = AllImage.DriverSix;
                    break;
                default:
                    imageUrl = AllImage.DriverOne;
                    break;
            }

            return imageUrl;
        }
    }
}