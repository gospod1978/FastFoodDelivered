﻿namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Web.ViewModels.Roles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRoleService roleServices;
        private readonly IUserService userServices;

        public RolesController(
            UserManager<ApplicationUser> userManager,
            IRoleService roleServices,
            IUserService userServices)
        {
            this.userManager = userManager;
            this.roleServices = roleServices;
            this.userServices = userServices;
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult Create()
        {
            var viewModel = new RoleCreateInputModel();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Create(RoleCreateInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var roleId = await this.roleServices.CreateAsyncRole(input.Name);
            this.TempData["InfoMessage"] = "Role created!";
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult All()
        {
            var viewModel = new RoleViewModel();

            var roles = this.roleServices.GetAll<RoleAllViewModel>();

            viewModel.Roles = roles;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public async Task<IActionResult> Delete(RoleAllViewModel input)
        {
            await this.roleServices.DeleteById(input.Id);

            this.TempData["InfoMessage"] = $"Role deleted!";
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
