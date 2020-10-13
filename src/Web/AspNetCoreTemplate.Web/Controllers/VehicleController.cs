namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Web.ViewModels.Vehicle;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class VehicleController : Controller
    {
        private readonly IVehicleService vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new VehicleCreateInputViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(VehicleCreateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var cityId = await this.vehicleService.CreateAsync(input.Name);
            this.TempData["InfoMessageVehicle"] = "Vehicle created!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = new ViewModels.Vehicle.VehicleIndexViewModel();

            var vehicles = this.vehicleService.GetAll<VehicleAll>();

            viewModel.Vehicles = vehicles;

            return this.View(viewModel);
        }
    }
}
