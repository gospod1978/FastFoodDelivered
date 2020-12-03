namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Locations;
    using AspNetCoreTemplate.Web.ViewModels.Streets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class LocationsController : Controller
    {
        private readonly IAreasService areasService;
        private readonly ICitiesService citiesService;
        private readonly IStreetsService streetsService;
        private readonly ILocationsService locationsService;

        public LocationsController(
            IAreasService areasService,
            ICitiesService citiesService,
            IStreetsService streetsService,
            ILocationsService locationsService)
        {
            this.areasService = areasService;
            this.citiesService = citiesService;
            this.streetsService = streetsService;
            this.locationsService = locationsService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateLocationsInputModel();

            var cities = this.citiesService.GetAllCities<CitiesDropDownMenuInStreet>();

            viewModel.Cities = cities;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateLocationsInputModel input)
        {
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(input.CityId);
            var viewModel = new CreateLocationsInputModel();
            viewModel.Areas = areas;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create1(CreateLocationsInputModel input)
        {
            var city = this.citiesService.GetById<CitiesDropDownMenuInStreet>(input.CityId);
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(input.CityId);
            var viewModel = new CreateLocationsInputModel();
            viewModel.Areas = areas;
            viewModel.CityName = city.CityName;
            viewModel.CityId = input.CityId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create2(CreateLocationsInputModel input)
        {
            var city = this.citiesService.GetById<CitiesDropDownMenuInStreet>(input.CityId);
            var area = this.areasService.GetById<AreasDropDownMenu>(input.AreaId);

            var streets = this.streetsService.GetAllStreets<StreetDropDownMenu>(input.AreaId);

            var viewModel = new CreateLocationsInputModel();

            viewModel.AreaName = area.AreaName;
            viewModel.CityName = city.CityName;
            viewModel.Streets = streets;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create3(CreateLocationsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var locationId = await this.locationsService.CreateAsyncLocation(input.Apartament, input.Number, input.Flour, input.Entrance, input.StreetId);
            this.TempData["InfoMessageAreas"] = "Location was created!";
            return this.RedirectToAction(nameof(this.NavBar));
        }

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }
    }
}
