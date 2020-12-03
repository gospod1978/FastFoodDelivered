namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using AspNetCoreTemplate.Web.ViewModels.Locations;
    using AspNetCoreTemplate.Web.ViewModels.Streets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AddressController : Controller
    {
        private readonly IAddressService addressService;
        private readonly IAreasService areasService;
        private readonly ICitiesService citiesService;
        private readonly IStreetsService streetsService;
        private readonly ILocationsService locationsService;

        public AddressController(
            IAddressService addressService,
            IAreasService areasService,
            ICitiesService citiesService,
            IStreetsService streetsService,
            ILocationsService locationsService)
        {
            this.addressService = addressService;
            this.areasService = areasService;
            this.citiesService = citiesService;
            this.streetsService = streetsService;
            this.locationsService = locationsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateAddressInputModel();

            var cities = this.citiesService.GetAllCities<CitiesDropDownMenuInStreet>();

            viewModel.Cities = cities;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateAddressInputModel input)
        {
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(input.CityId);
            var viewModel = new CreateAddressInputModel();
            viewModel.Areas = areas;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create1(CreateAddressInputModel input)
        {
            var city = this.citiesService.GetById<CitiesDropDownMenuInStreet>(input.CityId);
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(input.CityId);
            var viewModel = new CreateAddressInputModel();
            viewModel.Areas = areas;
            viewModel.CityName = city.CityName;
            viewModel.CityId = input.CityId;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create2(string id)
        {
            var city = this.areasService.GetCityByAreaId<AreasAll>(id);
            var area = this.areasService.GetById<AreasDropDownMenu>(id);

            var streets = this.streetsService.GetAllStreets<StreetDropDownMenu>(id);

            var viewModel = new CreateAddressInputModel();

            viewModel.AreaName = area.AreaName;
            viewModel.CityName = city.City.CityName;
            viewModel.Streets = streets;
            viewModel.CityId = city.CityId;
            viewModel.AreaId = id;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create2(CreateAddressInputModel input)
        {
            var city = this.areasService.GetCityByAreaId<AreasAll>(input.AreaId);
            var area = this.areasService.GetById<AreasDropDownMenu>(input.AreaId);

            var streets = this.streetsService.GetAllStreets<StreetDropDownMenu>(input.AreaId);

            var viewModel = new CreateAddressInputModel();

            viewModel.AreaName = area.AreaName;
            viewModel.CityName = city.City.CityName;
            viewModel.Streets = streets;
            viewModel.CityId = city.CityId;
            viewModel.AreaId = input.AreaId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create3(CreateAddressInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var locationId = await this.locationsService.CreateAsyncLocation(input.Apartament, input.Number, input.Flour, input.Entrance, input.StreetId);
            var address = await this.addressService.CreateAsyncAddress(input.CityId, input.AreaId, input.StreetId, locationId);
            this.TempData["InfoMessageAreas"] = "Address was created!";
            return this.RedirectToAction("Create", "LocationsObject", new { @addressId = address });
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var viewModel = this.addressService.GetById<DetailsAddressIndexViewModel>(id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult NavBarAddress()
        {
            return this.View();
        }
    }
}
