    namespace AspNetCoreTemplate.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.Addresses;
    using AspNetCoreTemplate.Services.Data.Orders;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.Users;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Categories;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Orders;
    using AspNetCoreTemplate.Web.ViewModels.Posts;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OrderController : Controller
    {
        private readonly IOrdersService ordersService;
        private readonly ICategoriesService categoriesService;
        private readonly IRestaurantService restaurantService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPictureService pictureService;
        private readonly IUsersDataService usersDataService;
        private readonly IUserService userService;
        private readonly ILocationsObjectService locationsObjectService;
        private readonly IPurchaseService purchaseService;
        private readonly IAddressService addressService;

        public OrderController(
            IOrdersService ordersService,
            ICategoriesService categoriesService,
            IRestaurantService restaurantService,
            UserManager<ApplicationUser> userManager,
            IPictureService pictureService,
            IUsersDataService usersDataService,
            IUserService userService,
            ILocationsObjectService locationsObjectService,
            IPurchaseService purchaseService,
            IAddressService addressService)
        {
            this.ordersService = ordersService;
            this.categoriesService = categoriesService;
            this.restaurantService = restaurantService;
            this.userManager = userManager;
            this.pictureService = pictureService;
            this.usersDataService = usersDataService;
            this.userService = userService;
            this.locationsObjectService = locationsObjectService;
            this.purchaseService = purchaseService;
            this.addressService = addressService;
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin, Restaurant")]
        public IActionResult MenuCreate()
        {
            var categories = this.categoriesService.GetAll<CategoryDropDownViewModels>();

            var viewModel = new OrderCreateInputViewModel();
            viewModel.Categories = categories;
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [Authorize(Roles = "Administrator, Admin, Restaurant")]
        public async Task<IActionResult> MenuCreate(OrderCreateInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userLogin = await this.userManager.GetUserAsync(this.User);
            var restorant = this.restaurantService.GetByUserId<InfoRestaurantModel>(userLogin.Id);

            var orderId = this.ordersService.CreateAsyncMenu(input.Name, input.Description, restorant.Id, input.TimePrepartion, input.Price, input.CategoryId);

            return this.RedirectToAction(nameof(this.Image), new { id = orderId.Result });
        }

        [Authorize]
        public IActionResult Image(string id)
        {
            var viewModel = new ViewModels.Orders.UploadImageViewModel();
            viewModel.OrderId = id;
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Image(IFormFile files, ViewModels.Orders.UploadImageViewModel input)
        {
            var target = new MemoryStream();

            string fileName = Path.GetFileNameWithoutExtension(input.Files.FileName);
            string extension = Path.GetExtension(input.Files.FileName);

            using (target)
            {
                await input.Files.CopyToAsync(target);
            }

            string uniqueFileName = fileName + extension;
            var imageId = await this.pictureService.CreateAsyncImage(uniqueFileName, extension, target.ToArray(), input.OrderId);
            this.ordersService.AddImageToOrder(input.OrderId, imageId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin, Restaurant")]
        public IActionResult Details(string id)
        {
            var viewModel = new OrderIndexViewModel();
            var imageOrder = this.pictureService.GetByOrderId<ViewModels.Orders.ImageDetailsViewModel>(id);
            var menu = this.ordersService.GetById<OrderDetailsViewModel>(id);
            var restorant = this.restaurantService.GetById<InfoRestaurantModel>(menu.RestaurantId);
            var restorantDetails = this.restaurantService.GetById<RestaurantAll>(menu.RestaurantId);
            UserIndexView userData = this.userService.GetById<UserIndexView>(restorant.UserId);
            var dataUser = this.usersDataService.GetByUserId<IndexDetailsViewModel>(userData.Id);
            var categoryName = this.categoriesService.GetById<CategoryViewModel>(menu.CategoryId);

            viewModel.OrderId = id;
            viewModel.Name = menu.Name;
            viewModel.RestaurantName = dataUser.Name;
            viewModel.Price = menu.Price;
            viewModel.TimePrepartion = menu.TimePrepartion;
            viewModel.Description = menu.Description;
            viewModel.Photo = imageOrder.DataFiles;
            viewModel.CategoryName = categoryName.Name;
            viewModel.Phone = restorantDetails.Phone;
            viewModel.Email = userData.Email;
            viewModel.AreaName = restorantDetails.Area.AreaName;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllByCategories(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var locationObjUserId = this.locationsObjectService.GetLocationByUserIdOnly<LocationObjectIndexViewModel>(user.Id);
            if (locationObjUserId == null)
            {
                return this.RedirectToAction("Create", "Address");
            }

            var address = this.addressService.GetById<DetailsAddressIndexViewModel>(locationObjUserId.AddressId);
            var userAreaName = address.AreaAreaName;
            var viewModel = new AllOrdersViewModel();

            var orders = this.ordersService.GetAllByCategoryName<OrderDetailsViewModel>(id);
            foreach (var order in orders)
            {
                var userIdrest = this.ordersService.GetById<OrderDetailsViewModel>(order.Id);
                var workingAreaRestaurant = this.userService.GetUserByRestaurantId(userIdrest.RestaurantId);
                var restorant = this.restaurantService.GetById<RestaurantAll>(workingAreaRestaurant);
                var restname = this.usersDataService.GetByUserId<UserDataIndexViewModel>(restorant.UserId);
                var locationRestaurantAreaName = restorant.Area.AreaName;
                var priceDelivery = this.purchaseService.PriceCourier(userAreaName, locationRestaurantAreaName);
                order.DeliveryPrice = priceDelivery;
                order.TotalPrice = order.Price + priceDelivery;
                order.RestaurantName = restname.Name;
            }

            viewModel.Orders = orders;

            return this.View(viewModel);
        }
    }
}
