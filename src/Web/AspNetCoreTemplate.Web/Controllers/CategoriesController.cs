namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Web.ViewModels.Categories;
    using AspNetCoreTemplate.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public CategoriesController(
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public IActionResult ByName(string name)
        {
            var viewModel = this.categoriesService.GetByName<CategoryViewModel>(name);

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Admin, Administrator, Restaurant")]
        public IActionResult Create()
        {
            var viewModel = new CategoryCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin, Administrator, Restaurant")]
        public async Task<IActionResult> Create(CategoryCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            string image = string.Empty;

            if (input.ImageUrl == null)
            {
                image = this.Image();
            }
            else
            {
                image = input.ImageUrl;
            }

            var categoryId = await this.categoriesService.CreateAsync(input.Name, input.Title, input.Description, image);
            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = new ViewModels.Home.IndexViewModel();

            var categories = this.categoriesService.GetAll<IndexCategoryViewModel>();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }

        private string Image()
        {
            string imageUrl;
            System.Random random = new System.Random();
            int number = random.Next(1, 8);

            switch (number)
            {
                case 1:
                    imageUrl = AllImage.Burgers;
                    break;
                case 2:
                    imageUrl = AllImage.Chinese;
                    break;
                case 3:
                    imageUrl = AllImage.FastFood;
                    break;
                case 4:
                    imageUrl = AllImage.Italian;
                    break;
                case 5:
                    imageUrl = AllImage.Pizza;
                    break;
                case 6:
                    imageUrl = AllImage.Pubs;
                    break;
                case 7:
                    imageUrl = AllImage.Sushi;
                    break;
                case 8:
                    imageUrl = AllImage.TakeWay;
                    break;
                default:
                    imageUrl = AllImage.TakeWay;
                    break;
            }

            return imageUrl;
        }
    }
}
