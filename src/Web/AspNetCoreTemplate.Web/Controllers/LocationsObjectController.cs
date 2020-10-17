using System;
using System.Threading.Tasks;
using AspNetCoreTemplate.Web.ViewModels.Addresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreTemplate.Web.Controllers
{
    public class LocationsObjectController : Controller
    {
        public LocationsObjectController()
        {
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new AddressCreateInputModel();

            return this.View(viewModel);
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Create(AddressCreateInputModel input)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.View(input);
        //    }
        //}

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }
    }
}
