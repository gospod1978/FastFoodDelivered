namespace AspNetCoreTemplate.Web.Areas.Identity.Pages.Account
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.User;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Logging;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger<RegisterModel> logger;
        private readonly IEmailSender emailSender;
        private readonly IRoleService roleService;
        private readonly IUserService userService;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRoleService roleService,
            IUserService userService,
            RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.emailSender = emailSender;
            this.roleService = roleService;
            this.userService = userService;
            this.roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            this.ReturnUrl = returnUrl;
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? this.Url.Content("~/");
            this.ExternalLogins = (await this.signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (this.ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = this.Input.UserName, Email = this.Input.Email };
                var userBaseIsEmpty = this.userManager.Users.Count();
                var userExisistEmail = this.userManager.Users.Where(x => x.Email == this.Input.Email).FirstOrDefault();
                var userNameExisist = this.userManager.Users.Where(x => x.UserName == this.Input.UserName).FirstOrDefault();

                if (userBaseIsEmpty == 0)
                {
                    var userNameAndRole = "Admin";
                    var roleExsist = this.roleManager.Roles.Where(x => x.Name == userNameAndRole).FirstOrDefault();

                    if (roleExsist == null)
                    {
                        var roleAdmin = this.roleService.CreateAsyncRole(userNameAndRole);
                    }

                    var userAdmin = new ApplicationUser { UserName = userNameAndRole, Email = "admin@admin.com" };
                    await this.userManager.CreateAsync(userAdmin, "admin2020");
                    await this.userService.AddRoleToUser<string>(userNameAndRole, userNameAndRole);
                }

                if (userExisistEmail == null && userNameExisist == null)
                {
                        var result = await this.userManager.CreateAsync(user, this.Input.Password);
                        if (result.Succeeded)
                        {
                            this.logger.LogInformation("User created a new account with password.");

                            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = this.Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                                protocol: this.Request.Scheme);

                            await this.emailSender.SendEmailAsync(this.Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                            if (this.userManager.Options.SignIn.RequireConfirmedAccount)
                            {
                                return this.RedirectToPage("RegisterConfirmation", new { email = this.Input.Email, returnUrl = returnUrl });
                            }
                            else
                            {
                                await this.signInManager.SignInAsync(user, isPersistent: false);
                                return this.LocalRedirect(returnUrl + "Address/Create");
                        }
                    }

                        foreach (var error in result.Errors)
                        {
                            this.ModelState.AddModelError(string.Empty, error.Description);
                        }
                }
                else
                {
                        if (userNameExisist != null)
                        {
                            this.ModelState.AddModelError("UserName", "UserName Already exists!");
                        }

                        if (userExisistEmail != null)
                        {
                            this.ModelState.AddModelError("Email", "Email Already exists!");
                        }

                        this.ReturnUrl = returnUrl;
                }
            }

            // If we got this far, something failed, redisplay form
            return this.Page();
        }
    }
}
