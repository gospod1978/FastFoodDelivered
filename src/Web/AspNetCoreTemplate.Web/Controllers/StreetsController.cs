namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Streets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class StreetsController : Controller
    {
        private readonly IAreasService areasService;
        private readonly ICitiesService citiesService;
        private readonly IStreetsService streetsService;

        public StreetsController(
            IAreasService areasService,
            ICitiesService citiesService,
            IStreetsService streetsService)
        {
            this.areasService = areasService;
            this.citiesService = citiesService;
            this.streetsService = streetsService;
        }

        [Authorize]
        public IActionResult Create1()
        {
            var viewModel = new CreateStreetInputModel();

            var cities = this.citiesService.GetAllCities<CitiesDropDownMenuInStreet>();

            viewModel.Cities = cities;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create1(CreateStreetInputModel input)
        {
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(input.CityId);
            var viewModel = new CreateStreetInputModel();
            viewModel.Areas = areas;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create2(CreateStreetInputModel input)
        {
            var city = this.citiesService.GetById<CitiesDropDownMenuInStreet>(input.CityId);
            var areas = this.areasService.GetAllAreas<AreasDropDownMenu>(input.CityId);
            var viewModel = new CreateStreetInputModel();
            viewModel.Areas = areas;
            viewModel.CityName = city.CityName;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create3(string id)
        {
            var city = this.citiesService.GetCityNameByAreaId(id);
            var areas = this.areasService.GetById<AreasDropDownMenu>(id);
            var viewModel = new CreateStreetInputModel();
            viewModel.AreaId = id;
            viewModel.AreaName = areas.AreaName;
            viewModel.CityName = city;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create3(CreateStreetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var streetID = await this.streetsService.CreateAsyncStreet(input.Name, input.AreaId);
            this.TempData["InfoMessageAreas"] = "Street was created!";
            return this.RedirectToAction("Create2", "Address", new { id = input.AreaId });
        }

        [Authorize]
        public IActionResult All(StreetByArea input)
        {
            var viewModel = new ViewModels.Streets.StreetIndexViewModel();

            var streets = this.streetsService.GetAllStreets<StreetsAll>(input.Id);

            viewModel.Streets = streets;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }
    }
}
