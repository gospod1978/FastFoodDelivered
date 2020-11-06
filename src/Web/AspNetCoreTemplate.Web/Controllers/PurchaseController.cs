namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Orders;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Orders;
    using AspNetCoreTemplate.Web.ViewModels.Purchase;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PurchaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IPurchaseService purchaseService;
        private readonly ILocationsObjectService locationsObjectService;
        private readonly IRestaurantService restaurantService;
        private readonly IAddressService addressService;
        private readonly IOrdersService ordersService;
        private readonly IUserService userService;

        public PurchaseController(
            UserManager<ApplicationUser> userManager,
            IPurchaseService purchaseService,
            ILocationsObjectService locationsObjectService,
            IRestaurantService restaurantService,
            IAddressService addressService,
            IOrdersService ordersService,
            IUserService userService)
        {
            this.userManager = userManager;
            this.purchaseService = purchaseService;
            this.locationsObjectService = locationsObjectService;
            this.restaurantService = restaurantService;
            this.addressService = addressService;
            this.ordersService = ordersService;
            this.userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Create(string id, string restaurantId, string promotionType, decimal menuPrice)
        {
            var orderId = id;
            var userLogin = await this.userManager.GetUserAsync(this.User);
            var locationObjUserId = this.locationsObjectService.GetAllByUserId<LocationObjectIndexViewModel>(userLogin.Id);
            var userIdrest = this.ordersService.GetById<OrderDetailsViewModel>(orderId);

            // var userIdByRestId = this.restaurantService.GetById<InfoRestaurantModel>(restaurantId);
            // var workingAreaRestaurant = this.addressService.GetByWorkingAreaByUserId(userIdrest.RestaurantId);
            var workingAreaRestaurant = this.userService.GetUserByRestaurantId(userIdrest.RestaurantId);
            var locationRestaurant = workingAreaRestaurant.AreaId;
            var courierPrice = this.purchaseService.FindCourier(locationRestaurant);
            var courierId = courierPrice.ElementAt(0).Key;
            var deliveryPrice = courierPrice.ElementAt(0).Value;

            var order = this.ordersService.GetById<OrderIndexViewModel>(orderId);

            var viewModel = new CreatePurchaseViewModel();
            viewModel.LocationsUser = locationObjUserId;
            viewModel.OrderId = orderId;
            viewModel.OrderName = order.Name;
            viewModel.ResaurantName = order.RestaurantName;
            viewModel.LocationRestaurantAreaId = locationRestaurant;
            viewModel.RestaurantId = restaurantId;
            viewModel.PromotionType = promotionType;
            viewModel.MenuPrice = menuPrice;
            viewModel.CourierId = courierId;
            viewModel.DeliveryPrice = deliveryPrice;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseViewModel input)
        {
            var userLogin = await this.userManager.GetUserAsync(this.User);

            var viewModel = new CreatePurchaseViewModel();
            var purchase = await this.purchaseService.CreateAsyncMenu(input.OrderId, userLogin.Id, input.CourierId, input.RestaurantId, input.PromotionType, input.MenuPrice, input.DeliveryPrice);

            return this.RedirectToAction(nameof(this.Details), new { id = purchase });
        }

        public IActionResult Details(string id)
        {
            return this.View();
        }
    }
}
