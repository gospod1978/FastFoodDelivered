namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Users;
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

        public CourierController(
            ICourierService courierService,
            IAddressService addressService,
            IVehicleService vehicleService,
            UserManager<ApplicationUser> userManager,
            ICitiesService citiesService,
            ILocationsObjectService locationsObjectService,
            IAreasService areasService,
            IUsersDataService usersDataService,
            IUserService userService)
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
        }

        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                var userLogin = await this.userManager.GetUserAsync(this.User);
                var curier = this.courierService.GetByUserId<CuriersAll>(userLogin.Id);
                id = curier.Id;
            }

            var couriers = this.courierService.GetById<CuriersAll>(id);
            var courierUser = this.courierService.GetById<InfoCurierModel>(id);
            var user = this.usersDataService.GetByUserId<CourierUserDataViewModel>(courierUser.UserId);
            var userData = this.userService.GetById<UserViewCourier>(courierUser.UserId);
            var vehichle = this.vehicleService.GetById<VehicleAll>(couriers.VehicleId);
            var workingArea = this.addressService.GetByWorkingAreaByUserId(courierUser.UserId);
            var area = this.areasService.GetById<AreasAll>(workingArea.AreaId);
            var viewModel = new CourierDetailsViewModel();

            viewModel.CourierName = user.Name;
            viewModel.City = area.City.CityName;
            viewModel.Birthday = couriers.Birthday.ToString();
            viewModel.Email = userData.Email;
            viewModel.Image = couriers.Image;
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
            var location = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(user.Id).ToList();
            var cityId = location.Where(x => x.UserId == user.Id).Select(c => c.Address.CityId).FirstOrDefault();
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CourierCreateInputViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string image = string.Empty;

            if (input.Image == null)
            {
                image = this.Image();
            }
            else
            {
                image = input.Image;
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
        public IActionResult All()
        {
            var viewModel = new ViewModels.Couriers.CourierViewModel();

            var couriers = this.courierService.GetAll<CuriersAll>();

            viewModel.Curiers = couriers;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AllCouriersDetail()
        {
            var viewModel = new ViewModels.Couriers.CourierAllViewModel();

            var couriers = this.courierService.GetAll<CuriersAll>();

            // var cities = this.addressService.GetAllCities<CitiesAll>();
            var vehciles = this.vehicleService.GetAll<VehicleAll>();

            viewModel.Curiers = couriers;

            // viewModel.Cities = cities;
            viewModel.Vehichles = vehciles;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult IsActive(string id)
        {
            return this.View();
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
