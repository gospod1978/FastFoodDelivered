namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CitiesController : Controller
    {
        private readonly ICitiesService citiesServices;

        public CitiesController(ICitiesService citiesServices)
        {
            this.citiesServices = citiesServices;
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult Create()
        {
            var viewModel = new CityCreateInputViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Create(CityCreateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var cityId = await this.citiesServices.CreateAsyncCity(input.CityName);
            this.TempData["InfoMessageCity"] = "City created!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult All()
        {
            var viewModel = new ViewModels.Cities.CityIndexViewModel();

            var cities = this.citiesServices.GetAllCities<CitiesAll>();

            viewModel.Cities = cities;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult NavBar()
        {
            return this.View();
        }
    }
}
