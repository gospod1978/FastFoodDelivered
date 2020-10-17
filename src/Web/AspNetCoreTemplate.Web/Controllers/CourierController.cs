namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
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

        public CourierController(
            ICourierService courierService,
            IAddressService addressService,
            IVehicleService vehicleService,
            UserManager<ApplicationUser> userManager)
        {
            this.courierService = courierService;
            this.addressService = addressService;
            this.vehicleService = vehicleService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var courierViewModel = this.courierService.GetById<CourierDetailsViewModel>(id);
            if (courierViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(courierViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CourierCreateInputViewModel();
            //var cities = this.addressService.GetAllCities<CitiesAll>();
            //var areas = this.addressService.GetAllAreas<WorkingAllArea>(cities.ToString());
            var vechiles = this.vehicleService.GetAll<VehicleAll>();
            //viewModel.Vechiles = vechiles;
            //viewModel.Areas = areas;

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

            DateTime dateBirthday;
            var birthday = DateTime.TryParseExact(input.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateBirthday);
            DateTime dateNow = DateTime.UtcNow;
            int age = new DateTime((dateNow - dateBirthday).Ticks).Year;

            if (age < 18)
            {
                this.TempData["InfoMessageCourier"] = "You need to be 18 years old!";
                return this.View(input);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var courierId = await this.courierService.CreateAsync(image, input.Phone, input.VechileId, input.Birthday, input.AreaId, user.Id);
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
            //var cities = this.addressService.GetAllCities<CitiesAll>();
            var vehciles = this.vehicleService.GetAll<VehicleAll>();

            viewModel.Curiers = couriers;
            //viewModel.Cities = cities;
            viewModel.Vehichles = vehciles;

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
                default: imageUrl = AllImage.DriverOne;
                    break;
            }

            return imageUrl;
        }
    }
}
