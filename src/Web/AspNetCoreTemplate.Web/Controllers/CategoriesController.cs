namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Categories;

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
        public IActionResult Create()
        {
            var viewModel = new CategoryCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CategoryCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var categoryId = await this.categoriesService.CreateAsync(input.Name, input.Title, input.Description, input.ImageUrl);
            this.TempData["InfoMessage"] = "Forum post created!";
            return this.RedirectToAction(nameof(this.Create));
        }

        public IActionResult All()
        {
            var viewModel = new ViewModels.Categories.CategoryIndexViewModel();

            var categories = this.categoriesService.GetAll<CategoryAll>();

            viewModel.Categories = categories;

            return this.View(viewModel);
        }
    }
}
