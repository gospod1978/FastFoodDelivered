namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using AspNetCoreTemplate.Web.ViewModels.Cities;
    using AspNetCoreTemplate.Web.ViewModels.Locations;
    using AspNetCoreTemplate.Web.ViewModels.Streets;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AddressController : Controller
    {
        private readonly IAddressService addressService;

        public AddressController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new AddressCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AddressCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            // var city = this.addressService.GetByNameCity<string>(input.CityName.ToUpper());
            var cityId = " ";

            //if (city == null)
            //{
            //   cityId = await this.addressService.CreateAsyncCity(input.CityName);
            //}
            //else
            //{
            //    cityId = city;
            //}

            //var area = this.addressService.GetByNameArea<AreasAll>(input.AreaName);
            var areaId = " ";

            //if (area == null)
            //{
            //    areaId = await this.addressService.CreateAsyncArea(input.AreaName, cityId, " ");
            //}
            //else
            //{
            //    areaId = area.Id;
            //}

            //var street = this.addressService.GetByNameStreets<StreetDetailsViewModel>(input.StreetName);
            var streetId = " ";

            //if (street == null)
            //{
            //    streetId = await this.addressService.CreateAsyncStreet(input.StreetName, areaId);
            //}
            //else
            //{
            //    streetId = street.Id;
            //}

            //var location = this.addressService.GetByNameLocations<LocationDetailsViewModel>(input.Number);

            //if (input.Apartament.ToString() == null)
            //{
            //    input.Apartament = 0;
            //}

            //if (input.Flour.ToString() == null)
            //{
            //    input.Flour = 0;
            //}

            //if (input.Entrance.ToString() == null)
            //{
            //    input.Entrance = 0;
            //}

            //if (location == null)
            //{
            //    await this.addressService.CreateAsyncLocation(input.Apartament, input.Number, input.Flour, input.Entrance, streetId);
            //}

            //cityId = await this.addressService.CreateAsyncCity(input.CityName);
            //areaId = await this.addressService.CreateAsyncArea(input.AreaName, cityId, " ");
            //streetId = await this.addressService.CreateAsyncStreet(input.StreetName, areaId);
            //var locId = await this.addressService.CreateAsyncLocation(input.Apartament, input.Number, input.Flour, input.Entrance, streetId);

            // var addressId = await this.addressService.CreateAsyncAddress(cityId, areaId, streetId, locId);
            var addressId = await this.addressService.CreateAsyncAddress("6d3ff375-f98a-4bec-a5c8-ced34bbb7332", "0791be20-5096-438e-b774-a98fc3f53dec", "054bf199-22ad-4fff-ba6e-daa59b595ae2", "69eb3e0e-9bf9-41bd-af3e-e57866aadddd");
            this.TempData["InfoMessageAreas"] = "Address was created!";
            return this.RedirectToAction(nameof(this.CreateLocation), new { id = addressId });
        }

        [Authorize]
        public IActionResult CreateLocation(string id)
        {
            var addressId = "9aff8807-1702-4926-8de6-a368586f2a9e";

            var viewModel = new LocationCreateInputModel();
            viewModel.AddressId = addressId;
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            //var locationCreate = await this.addressService.CreateAsyncLocationObject(input.Name, input.AddressId);
            this.TempData["InfoMessageLocationObject"] = "Location was created!";
            return this.RedirectToAction(nameof(this.AllLocation));
        }

        [Authorize]
        public IActionResult AllLocation()
        {
            var viewModel = new LocationIndexViewModel();
            //var locationObject = this.addressService.GetAllLocationObjects();
            return this.View();
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
