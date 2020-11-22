namespace AspNetCoreTemplate.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.Orders;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.LocationObjects;
    using AspNetCoreTemplate.Web.ViewModels.Orders;
    using AspNetCoreTemplate.Web.ViewModels.Purchases;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
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
        private readonly IUsersDataService usersDataService;
        private readonly IImagesService pictureService;
        private readonly ICourierService courierService;

        public PurchaseController(
            UserManager<ApplicationUser> userManager,
            IPurchaseService purchaseService,
            ILocationsObjectService locationsObjectService,
            IRestaurantService restaurantService,
            IAddressService addressService,
            IOrdersService ordersService,
            IUserService userService,
            IUsersDataService usersDataService,
            IImagesService pictureService,
            ICourierService courierService)
        {
            this.userManager = userManager;
            this.purchaseService = purchaseService;
            this.locationsObjectService = locationsObjectService;
            this.restaurantService = restaurantService;
            this.addressService = addressService;
            this.ordersService = ordersService;
            this.userService = userService;
            this.usersDataService = usersDataService;
            this.pictureService = pictureService;
            this.courierService = courierService;
        }

        [Authorize]
        public async Task<IActionResult> Create(string id)
        {
            var orderId = id;
            var userLogin = await this.userManager.GetUserAsync(this.User);
            var locationObjUserId = this.locationsObjectService.GetLocationByUserIdOnly<LocationObjectIndexViewModel>(userLogin.Id);
            var userIdrest = this.ordersService.GetById<OrderDetailsViewModel>(orderId);
            var workingAreaRestaurant = this.userService.GetUserByRestaurantId(userIdrest.RestaurantId);
            var restorant = this.restaurantService.GetById<RestaurantAll>(workingAreaRestaurant);
            var locationRestaurantAreaName = restorant.Area.AreaName;
            var userAreaName = locationObjUserId.Address.Area.AreaName;
            var restName = this.restaurantService.GetById<RestaurantAll>(restorant.Id);
            var restname = this.usersDataService.GetByUserId<UserDataIndexViewModel>(restName.UserId);
            var priceDelivery = this.purchaseService.PriceCourier(userAreaName, locationRestaurantAreaName);
            var courierId = this.purchaseService.CourierIdFind(restorant.AreaId);

            var order = this.ordersService.GetById<OrderDetailsViewModel>(orderId);

            var viewModel = new CreatePurchaseViewModel();
            viewModel.LocationsUser = locationObjUserId.Name;
            viewModel.OrderId = orderId;
            viewModel.OrderName = order.Name;
            viewModel.ResaurantName = restname.Name;
            viewModel.RestaurantId = restorant.Id;
            viewModel.LocationRestaurantAreaId = restorant.AreaId;
            viewModel.PromotionType = userIdrest.PromotionType.ToString();
            viewModel.MenuPrice = order.Price;
            viewModel.CourierId = courierId;
            viewModel.DeliveryPrice = priceDelivery;

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

        [Authorize]
        public IActionResult Details(string id)
        {
            var purchase = this.purchaseService.GetById<DetailsPurchaseViewModel>(id);
            var viewModel = new DetailsPurchaseViewModel();
            var user = this.usersDataService.GetByUserId<CourierUserDataViewModel>(purchase.UserId);
            var restName = this.restaurantService.GetById<RestaurantAll>(purchase.RestaurantId);
            var restname = this.usersDataService.GetByUserId<UserDataIndexViewModel>(restName.UserId);
            var courier = this.courierService.GetById<CourierWaitApprove>(purchase.CourierId);
            var couriername = this.usersDataService.GetByUserId<UserDataIndexViewModel>(courier.UserId);
            var photo = this.pictureService.GetAllImagesByUser<ViewModels.UsersData.ImageDetailsViewModel>(couriername.Id).FirstOrDefault();
            viewModel.Id = purchase.Id;
            viewModel.OrderId = purchase.OrderId;
            viewModel.OrderName = purchase.OrderName;
            viewModel.UserId = purchase.UserId;
            viewModel.RestaurantId = purchase.RestaurantId;
            viewModel.RestaurantName = restname.Name;
            viewModel.CourierId = purchase.CourierId;
            viewModel.CourierName = couriername.Name;
            viewModel.Status = purchase.Status;
            viewModel.PromotionType = purchase.PromotionType;
            viewModel.Price = purchase.Price;
            viewModel.MenuPrice = purchase.MenuPrice;
            viewModel.DeliveryPrice = purchase.DeliveryPrice;
            viewModel.Photo = photo.DataFiles;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> AllByStatus()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var role = string.Empty;
            if (this.User.IsInRole(GlobalConstants.CourierRoleName))
            {
                role = GlobalConstants.CourierRoleName;
            }
            else if (this.User.IsInRole(GlobalConstants.RestaurantRoleName))
            {
                role = GlobalConstants.RestaurantRoleName;
            }
            else if (this.User.IsInRole(GlobalConstants.AdministratorRoleName) || this.User.IsInRole(GlobalConstants.AdminRoleName))
            {
                role = GlobalConstants.AdminRoleName;
            }

            var purchases = this.purchaseService.GetAll<DetailsPurchaseViewModel>(user.Id, role);
            var viewModel = new AllPurchaseViewModel();
            viewModel.Purchases = purchases;

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Purchase()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var purchases = this.purchaseService.GetAllByUserId<DetailsPurchaseViewModel>(user.Id);
            foreach (var item in purchases)
            {
                var purchase = this.purchaseService.GetById<DetailsPurchaseViewModel>(item.Id);
                var orderName = this.ordersService.GetById<OrderDetailsViewModel>(purchase.OrderId);
                var restName = this.restaurantService.GetById<RestaurantAll>(purchase.RestaurantId);
                var restname = this.usersDataService.GetByUserId<UserDataIndexViewModel>(restName.UserId);
                var courier = this.courierService.GetById<CourierWaitApprove>(purchase.CourierId);
                var couriername = this.usersDataService.GetByUserId<UserDataIndexViewModel>(courier.UserId);
                item.RestaurantName = restname.Name;
                item.CourierName = couriername.Name;
                item.OrderName = orderName.Name;
            }

            var viewModel = new AllPurchaseViewModel();
            viewModel.Purchases = purchases;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Status(string id)
        {
            var purchase = this.purchaseService.GetById<DetailsPurchaseViewModel>(id);
            var viewModel = new DetailsPurchaseViewModel();
            viewModel.OrderName = purchase.OrderName;
            viewModel.Status = purchase.Status;
            viewModel.NewStatus = this.NextStatus(purchase.Status);
            viewModel.Id = id;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Status(DetailsPurchaseViewModel input)
        {
            this.purchaseService.ChangeStatus(input.Id, input.NewStatus);

            return this.RedirectToAction(nameof(this.AllByStatus));
        }

        private string NextStatus(string status)
        {
            string newStatus = string.Empty;
            if (status == "Purchaise")
            {
                newStatus = "PreparedRestaurant";
            }
            else if (status == "PreparedRestaurant")
            {
                newStatus = "Delivered";
            }
            else if (status == "Delivered")
            {
                newStatus = "InClient";
            }
            else if (status == "InClient")
            {
                newStatus = "Unpaid";
            }
            else if (status == "Unpaid")
            {
                newStatus = "Paid";
            }
            else
            {
                newStatus = "Finished";
            }

            return newStatus;
        }
    }
}
