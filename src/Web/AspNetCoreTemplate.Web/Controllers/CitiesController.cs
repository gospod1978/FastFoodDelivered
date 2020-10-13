namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CitiesController : Controller
    {
        private readonly IAddressService addressServices;

        public CitiesController(IAddressService addressServices)
        {
            this.addressServices = addressServices;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CityCreateInputViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CityCreateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var cityId = await this.addressServices.CreateAsyncCity(input.CityName);
            this.TempData["InfoMessageCity"] = "City created!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = new ViewModels.Cities.CityIndexViewModel();

            var cities = this.addressServices.GetAllCities<CitiesAll>();

            viewModel.Cities = cities;

            return this.View(viewModel);
        }
    }
}
