namespace AspNetCoreTemplate.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Areas;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AreasController : Controller
    {
        private readonly IAreasService areasService;
        private readonly ICitiesService citiesService;

        public AreasController(
            IAreasService areasService,
            ICitiesService citiesService)
        {
            this.areasService = areasService;
            this.citiesService = citiesService;
        }

        [Authorize]
        public IActionResult Create()
        {
            var cities = this.citiesService.GetAllCities<CityDropDownViewModels>();

            var viewModel = new AreaCreateInputModel
            {
                Cities = cities,
            };
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(AreaCreateInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var areaId = await this.areasService.CreateAsyncArea(input.AreaName.ToString(), input.CityId, input.Image);
            this.TempData["InfoMessageAreas"] = "Area was created!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> CreateAllAreaInSofia()
        {
            var cities = this.citiesService.GetAllCities<CityDropDownViewModels>();
            var cityId = string.Empty;

            foreach (var city in cities)
            {
                if (city.CityName == "SOFIA")
                {
                    cityId = city.Id;
                }
            }

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var areas = this.Image();

            for (int i = 0; i < areas.Count; i++)
            {
                string image = areas[i + 1][0];
                string areaName = areas[i + 1][1];
                var area = await this.areasService.CreateAsyncArea(areaName, cityId, image);
            }

            this.TempData["InfoMessageAreas"] = "Areas Sofia was created!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult All(AreasByCity input)
        {
            var viewModel = new ViewModels.Areas.AreaIndexViewModel();

            var areas = this.areasService.GetAllAreas<AreasAll>(input.Id);

            viewModel.Areas = areas;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }

        private Dictionary<int, List<string>> Image()
        {
            var images = new Dictionary<int, List<string>>();
            images.Add(1, new List<string> { AllImage.Sredec, "Sredec" });
            images.Add(2, new List<string> { AllImage.KrasnoSelo, "KrasnoSelo" });
            images.Add(3, new List<string> { AllImage.Vazrajdane, "Vazrajdane" });
            images.Add(4, new List<string> { AllImage.Oborishte, "Oborishte" });
            images.Add(5, new List<string> { AllImage.Serdika, "Serdika" });
            images.Add(6, new List<string> { AllImage.Poduene, "Poduene" });
            images.Add(7, new List<string> { AllImage.Slatina, "Slatina" });
            images.Add(8, new List<string> { AllImage.Izgrev, "Izgrev" });
            images.Add(9, new List<string> { AllImage.Lozenetz, "Lozenetz" });
            images.Add(10, new List<string> { AllImage.Triadica, "Triadica" });
            images.Add(11, new List<string> { AllImage.KrasnaPolqna, "KrasnaPolqna" });
            images.Add(12, new List<string> { AllImage.Ilinden, "Ilinden" });
            images.Add(13, new List<string> { AllImage.Nadejda, "Nadejda" });
            images.Add(14, new List<string> { AllImage.Iskar, "Iskar" });
            images.Add(15, new List<string> { AllImage.Mladost, "Mladost" });
            images.Add(16, new List<string> { AllImage.Studentski, "Studentski" });
            images.Add(17, new List<string> { AllImage.Vitosha, "Vitosha" });
            images.Add(18, new List<string> { AllImage.OvchaKupel, "OvchaKupel" });
            images.Add(19, new List<string> { AllImage.Lulin, "Lulin" });
            images.Add(20, new List<string> { AllImage.Vrabnica, "Vrabnica" });
            images.Add(21, new List<string> { AllImage.NoviIskar, "NoviIskar" });
            images.Add(22, new List<string> { AllImage.Kremikovci, "Kremikovci" });
            images.Add(23, new List<string> { AllImage.Pancharevo, "Pancharevo" });
            images.Add(24, new List<string> { AllImage.Bankq, "Bankq" });

            return images;
        }
    }
}
