namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using AspNetCoreTemplate.Web.ViewModels.Streets;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using AspNetCoreTemplate.Web.ViewModels.Vehicle;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CourierController : Controller
    {
        private readonly ICourierService courierService;
        private readonly IAddressService addressService;
        private readonly IVehicleService vehicleService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICitiesService citiesService;
        private readonly ILocationsObjectService locationsObjectService;
        private readonly IAreasService areasService;
        private readonly IUsersDataService usersDataService;
        private readonly IUserService userService;
        private readonly IImagesService imagesService;
        private readonly IRestaurantService restaurantService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public CourierController(
            ICourierService courierService,
            IAddressService addressService,
            IVehicleService vehicleService,
            UserManager<ApplicationUser> userManager,
            ICitiesService citiesService,
            ILocationsObjectService locationsObjectService,
            IAreasService areasService,
            IUsersDataService usersDataService,
            IUserService userService,
            IImagesService imagesService,
            IRestaurantService restaurantService,
            RoleManager<ApplicationRole> roleManager)
        {
            this.courierService = courierService;
            this.addressService = addressService;
            this.vehicleService = vehicleService;
            this.userManager = userManager;
            this.citiesService = citiesService;
            this.locationsObjectService = locationsObjectService;
            this.areasService = areasService;
            this.usersDataService = usersDataService;
            this.userService = userService;
            this.imagesService = imagesService;
            this.restaurantService = restaurantService;
            this.roleManager = roleManager;
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin, Courier")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                var userLogin = await this.userManager.GetUserAsync(this.User);
                var curier = this.courierService.GetByUserId<CuriersAll>(userLogin.Id);
                if (curier == null)
                {
                    return this.RedirectToAction("AllCouriers", "UsersData");
                }

                id = curier.Id;
            }

            var couriers = this.courierService.GetById<CuriersAll>(id);
            var courierUser = this.courierService.GetById<InfoCurierModel>(id);
            var user = this.usersDataService.GetByUserId<CourierUserDataViewModel>(courierUser.UserId);
            var userData = this.userService.GetById<UserViewCourier>(courierUser.UserId);
            var vehichle = this.vehicleService.GetById<VehicleAll>(couriers.VehicleId);
            var workingArea = this.addressService.GetByWorkingAreaByUserId(courierUser.UserId);
            var area = this.areasService.GetById<AreasAll>(workingArea.AreaId);
            var photo = this.imagesService.GetAllImagesByUser<ImageDetailsViewModel>(user.Id).FirstOrDefault();
            var viewModel = new CourierDetailsViewModel();

            viewModel.CourierName = user.Name;
            viewModel.City = area.City.CityName;
            viewModel.Birthday = couriers.Birthday.ToString();
            viewModel.Email = userData.Email;
            viewModel.Photo = photo.DataFiles;
            viewModel.ImageName = photo.Name;
            viewModel.Id = id;
            viewModel.Phone = couriers.Phone;
            viewModel.Vehicle = vehichle.Name;
            viewModel.WorkingArea = area.AreaName;
            viewModel.IsActiv = workingArea.ActiveWorkingArea.ToString();
            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create1()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (user.Roles.Count() != 0)
            {
                this.TempData["InfoMessageApply"] = $"You are {user.Roles}!";
                return this.RedirectToAction("Index", "Home");
            }

            var restaurant = this.restaurantService.GetByUserId<InfoRestaurantModel>(user.Id);

            if (restaurant != null)
            {
                this.TempData["InfoMessageApply"] = "You are apliade restaurant!";
                return this.RedirectToAction("Index", "Home");
            }

            var courier = this.courierService.GetByUserId<InfoCurierModel>(user.Id);

            if (courier != null)
            {
                this.TempData["InfoMessageApply"] = "You are allready apliead for Courier!";
                return this.RedirectToAction("Index", "Home");
            }

            var location = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(user.Id).ToList();
            var cityId = location.Where(x => x.UserId == user.Id).Select(c => c.Address.CityId).FirstOrDefault();

            if (cityId == null)
            {
                return this.RedirectToAction("Create", "Address");
            }

            var city = this.citiesService.GetById<CitiesAll>(cityId);
            var cityName = city.CityName;

            return location.Count == 0 ? this.RedirectToAction("Create", "Address") : this.RedirectToAction(nameof(this.Create), new { name = cityName, id = city.Id });
        }

        [Authorize]
        public IActionResult Create(string name, string id)
        {
            var vechiles = this.vehicleService.GetAll<VehicleAll>();
            var areas = this.areasService.GetAllAreas<AreasAll>(id);

            var viewModel = new CourierCreateInputViewModel();

            viewModel.Vechiles = vechiles;
            viewModel.Areas = areas;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Create2(string name, string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var location = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(user.Id).ToList();
            var cityId = location.Where(x => x.UserId == user.Id).Select(c => c.Address.CityId).FirstOrDefault();
            var areas = this.areasService.GetAllAreas<AreasAll>(cityId);
            var vechiles = this.vehicleService.GetAll<VehicleAll>();
            var image = this.imagesService.GetById<ImageDetailsViewModel>(id);

            var viewModel = new CourierCreateInputViewModel();
            var stream = new MemoryStream(image.DataFiles);
            viewModel.Areas = areas;
            viewModel.Photo = image.DataFiles;
            viewModel.ImageName = image.Name;
            viewModel.PhotoId = id;
            viewModel.Vechiles = vechiles;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create2(CourierCreateInputViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string image = string.Empty;

            if (input.Photo == null)
            {
                image = this.Image();
            }
            else
            {
                image = input.PhotoId;
            }

            DateTime dateNow = DateTime.UtcNow;
            int age = new DateTime((dateNow - input.Birthday).Ticks).Year;

            if (age < 18)
            {
                this.TempData["InfoMessageCourier"] = "You need to be 18 years old!";
                return this.View(input);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var workingArea = this.addressService.CreateAsyncWorkingArea(input.AreaId, user.Id);

            var courierId = await this.courierService.CreateAsync(image, input.Phone, input.VechileId, input.Birthday, workingArea.Result, user.Id, input.AreaId);
            this.TempData["InfoMessageCourier"] = "You applied for Courier!";
            return this.RedirectToAction(nameof(this.Details), new { id = courierId });
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult All()
        {
            var viewModel = new ViewModels.Couriers.CourierViewModel();

            var couriers = this.courierService.GetAll<CuriersAll>();

            viewModel.Curiers = couriers;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult AllCouriersDetail()
        {
            var viewModel = new ViewModels.Couriers.CourierAllViewModel();

            var couriers = this.courierService.GetAll<CuriersAll>();

            var vehciles = this.vehicleService.GetAll<VehicleAll>();

            viewModel.Curiers = couriers;

            viewModel.Vehichles = vehciles;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin, Courier")]
        public IActionResult IsActive(string id)
        {
            var courierDetail = this.courierService.GetById<CourierDetailsViewModel>(id);
            var city = this.areasService.GetById<AreasAll>(courierDetail.AreaId);
            var cityName = city.City.CityName;
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(city.CityId);
            var viewModel = new ChangeWorkingAreaIdViewModel();
            viewModel.Areas = areas;
            viewModel.CityName = cityName;
            viewModel.CourierId = id;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [Authorize(Roles = "Administrator, Admin, Courier")]
        public async Task<IActionResult> IsActive(ChangeWorkingAreaIdViewModel input)
        {
            await this.courierService.CreateWorkingAreaByCourierId(input.CourierId, input.AreaId);

            return this.RedirectToAction(nameof(this.Details), new { id = input.CourierId });
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
                default: imageUrl = AllImage.DriverOne;
                    break;
            }

            return imageUrl;
        }
    }
}
