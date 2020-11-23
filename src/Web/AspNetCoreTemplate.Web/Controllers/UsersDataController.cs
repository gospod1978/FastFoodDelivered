namespace AspNetCoreTemplate.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Data.Restaurant;
    using AspNetCoreTemplate.Services.Data.UserService;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using AspNetCoreTemplate.Web.ViewModels.Restaurants;
    using AspNetCoreTemplate.Web.ViewModels.Users;
    using AspNetCoreTemplate.Web.ViewModels.UsersData;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    public class UsersDataController : Controller
    {
        private readonly IUserService userService;
        private readonly IUsersDataService usersDataService;
        private readonly IDocumentsService documentsService;
        private readonly IImagesService imagesService;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ICourierService courierService;
        private readonly ICitiesService citiesService;
        private readonly IRestaurantService restaurantService;

        public UsersDataController(
            IUserService userService,
            IUsersDataService usersDataService,
            IDocumentsService documentsService,
            IImagesService imagesService,
            IWebHostEnvironment hostEnvironment,
            ICourierService courierService,
            ICitiesService citiesService,
            IRestaurantService restaurantService)
        {
            this.userService = userService;
            this.usersDataService = usersDataService;
            this.documentsService = documentsService;
            this.imagesService = imagesService;
            this.hostEnvironment = hostEnvironment;
            this.courierService = courierService;
            this.citiesService = citiesService;
            this.restaurantService = restaurantService;
        }

        [Authorize]
        public IActionResult NavBar()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Index(string name, string id, string userName, string userDataId)
        {
            if (id != null)
            {
                userName = this.userService.GetById<UserIndexView>(id).UserName;
                name = this.usersDataService.GetName(id);
                userDataId = this.usersDataService.GetId(id);
            }
            else if (userDataId != null)
            {
                id = this.usersDataService.GetByDataId(userDataId);
                userName = this.userService.GetById<UserIndexView>(id).UserName;
                name = this.usersDataService.GetName(id);
            }
            else
            {
                if (id == null && name == null && userName == null)
                {
                    userName = this.User.Identity.Name.ToString();
                    id = this.userService.GetUserName(userName);
                    name = this.usersDataService.GetName(id);
                    userDataId = this.usersDataService.GetId(id);
                }
                else
                {
                    userName = this.User.Identity.Name.ToString();
                    id = this.userService.GetUserName(userName);
                    userDataId = this.usersDataService.GetId(id);
                }
            }

            var documents = this.documentsService.GetAllDocumentsByUser<DocumentDetails>(userDataId);
            var viewModel = new IndexDetailsViewModel();
            viewModel.Name = name;
            viewModel.UserId = id;
            viewModel.UserName = userName;
            viewModel.Documents = (System.Collections.Generic.ICollection<DocumentDetails>)documents;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult IndexPdf(string name, string id, string userName)
        {
            var userDataid = string.Empty;

            if (id == null && name == null && userName == null)
            {
                userName = this.User.Identity.Name.ToString();
                id = this.userService.GetUserName(userName);
                name = this.usersDataService.GetName(id);
                userDataid = this.usersDataService.GetId(id);
            }
            else
            {
                userName = this.User.Identity.Name.ToString();
                id = this.userService.GetUserName(userName);
                userDataid = this.usersDataService.GetId(id);
            }

            var documents = this.documentsService.GetAllDocumentsByUser<DocumentDetails>(userDataid);
            var viewModel = new IndexDetailsViewModel();
            viewModel.Name = name;
            viewModel.UserId = id;
            viewModel.UserName = userName;
            viewModel.Documents = (System.Collections.Generic.ICollection<DocumentDetails>)documents;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var userName = this.User.Identity.Name.ToString();
            var id = this.userService.GetUserName(userName);
            var viewModel = new CreateUserDataViewModel();
            viewModel.UserUserName = userName;
            viewModel.UserId = id;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDataViewModel input)
        {
            var name = input.Name;
            var userName = this.User.Identity.Name.ToString();
            var id = this.userService.GetUserName(userName);

            var data = await this.usersDataService.CreateAsyncUserData(input.Name, id);
            this.TempData["InfoMessageAreas"] = "Data was created!";
            return this.RedirectToAction(nameof(this.Index), new { @name = input.Name });
        }

        [Authorize]
        public IActionResult Uploaud(string id)
        {
            var viewModel = new UploadDocumentViewModel();
            var userDataId = this.usersDataService.GetByUserId<UserDataIndexViewModel>(id);
            viewModel.UserDataId = userDataId.Id;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Uploaud1(IFormFile files, UploadDocumentViewModel input)
        {
            if (input.UserDataId == null)
            {
                var userName = this.User.Identity.Name.ToString();
                var id = this.userService.GetUserName(userName);
                input.UserDataId = this.usersDataService.GetId(id);
            }

            if (files != null)
            {
                if (files.Length > 0)
                {
                    var newName = files.FileName;
                    var indexOf = newName.IndexOf('.');
                    var exstention = newName.Substring(indexOf + 1);
                    var target = new MemoryStream();

                    using (target)
                    {
                        files.CopyTo(target);
                    }

                    var objfiles = await this.documentsService.CreateAsyncDocument(newName, exstention, target.ToArray(), input.UserDataId);
                }
            }

            var userId = this.usersDataService.GetByDataId(input.UserDataId);

            return this.RedirectToAction(nameof(this.Index), new { id = userId });
        }

        public IActionResult ViewDocument(string id)
        {
            var document = this.documentsService.GetByIdDelete<DocumentDetails>(id);
            var target = new MemoryStream(document.DataFiles);
            var pdfDocument = new FileStreamResult(target, "application/pdf");
            return pdfDocument;
        }

        [Authorize]
        public IActionResult IndexPdfOne(string id)
        {
            var viewModel = new UserDocumentPdfDetail();
            var document = this.documentsService.GetByIdDelete<DocumentDetails>(id);
            var userDocument = document.UserData;
            var realName = userDocument.Name;
            var userId = this.usersDataService.GetByDataId(document.UserDataId);

            viewModel.Name = document.Name;
            viewModel.Id = id;
            viewModel.FileType = document.FileType;
            viewModel.DataFiles = document.DataFiles;
            viewModel.CreatedOn = document.CreatedOn;
            viewModel.RaelName = realName;
            viewModel.UserDataId = userId;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Download(string id)
        {
            var document = this.documentsService.GetById<DocumentDetails>(id);
            var target = new MemoryStream(document.DataFiles);
            var pdfDocument = new FileStreamResult(target, "application/pdf");
            pdfDocument.FileDownloadName = document.Name;

            return pdfDocument;
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult AskDelete(string id)
        {
            var document = this.documentsService.GetById<DocumentDeleted>(id);

            var viewModel = new DocumentDeleted();
            viewModel.Id = id;
            viewModel.Name = document.Name;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult Delete(string id)
        {
            var document = this.documentsService.GetById<DocumentDeleted>(id);
            var userDataId = document.UserDataId;
            this.documentsService.DeleteById(id);

            return this.RedirectToAction(nameof(this.Index), new { userDataId = userDataId });
        }

        [Authorize]
        public IActionResult NoActive(string name, string id, string userName, string userDataId)
        {
            if (id != null)
            {
                userName = this.userService.GetById<UserIndexView>(id).UserName;
                name = this.usersDataService.GetName(id);
                userDataId = this.usersDataService.GetId(id);
            }
            else if (userDataId != null)
            {
                id = this.usersDataService.GetByDataId(userDataId);
                userName = this.userService.GetById<UserIndexView>(id).UserName;
                name = this.usersDataService.GetName(id);
            }
            else
            {
                if (id == null && name == null && userName == null)
                {
                    userName = this.User.Identity.Name.ToString();
                    id = this.userService.GetUserName(userName);
                    name = this.usersDataService.GetName(id);
                    userDataId = this.usersDataService.GetId(id);
                }
                else
                {
                    userName = this.User.Identity.Name.ToString();
                    id = this.userService.GetUserName(userName);
                    userDataId = this.usersDataService.GetId(id);
                }
            }

            var documents = this.documentsService.GetAllDocumentsByUserNoActive<DocumentDetails>(userDataId);
            var viewModel = new IndexDetailsViewModel();
            viewModel.Name = name;
            viewModel.UserId = id;
            viewModel.UserName = userName;
            viewModel.Documents = (System.Collections.Generic.ICollection<DocumentDetails>)documents;

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Image()
        {
            var viewModel = new UploadImageViewModel();
            var userName = this.User.Identity.Name.ToString();
            var id = this.userService.GetUserName(userName);
            var userDataId = this.usersDataService.GetByUserId<UserDataIndexViewModel>(id);

            if (userDataId == null)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            viewModel.UserDataId = userDataId.Id;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Image1(IFormFile files, UploadImageViewModel input)
        {
            if (input.UserDataId == null)
            {
                var userName = this.User.Identity.Name.ToString();
                var id = this.userService.GetUserName(userName);
                input.UserDataId = this.usersDataService.GetId(id);
            }

            var images = this.imagesService.GetAllImagesByUser<ImageDetailsViewModel>(input.UserDataId);
            if (images != null)
            {
                foreach (var image in images)
                {
                    this.imagesService.DeleteById(image.Id);
                }
            }

            var target = new MemoryStream();

            string fileName = Path.GetFileNameWithoutExtension(input.Files.FileName);
            string extension = Path.GetExtension(input.Files.FileName);

            using (target)
            {
                await input.Files.CopyToAsync(target);
            }

            string uniqueFileName = fileName + extension;
            var imageId = await this.imagesService.CreateAsyncImage(uniqueFileName, extension, target.ToArray(), input.UserDataId);

            return this.RedirectToAction("Create3", "Restaurant", new { id = imageId, userId = input.UserDataId });
        }

        [Authorize]
        public IActionResult Image2()
        {
            var viewModel = new UploadImageViewModel();
            var userName = this.User.Identity.Name.ToString();
            var id = this.userService.GetUserName(userName);
            var userDataId = this.usersDataService.GetByUserId<UserDataIndexViewModel>(id);

            if (userDataId == null)
            {
                return this.RedirectToAction(nameof(this.Create));
            }

            viewModel.UserDataId = userDataId.Id;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Image3(IFormFile files, UploadImageViewModel input)
        {
            if (input.UserDataId == null)
            {
                var userName = this.User.Identity.Name.ToString();
                var id = this.userService.GetUserName(userName);
                input.UserDataId = this.usersDataService.GetId(id);
            }

            var images = this.imagesService.GetAllImagesByUser<ImageDetailsViewModel>(input.UserDataId);
            if (images != null)
            {
                foreach (var image in images)
                {
                    this.imagesService.DeleteById(image.Id);
                }
            }

            var target = new MemoryStream();

            string fileName = Path.GetFileNameWithoutExtension(input.Files.FileName);
            string extension = Path.GetExtension(input.Files.FileName);

            using (target)
            {
                await input.Files.CopyToAsync(target);
            }

            string uniqueFileName = fileName + extension;
            var imageId = await this.imagesService.CreateAsyncImage(uniqueFileName, extension, target.ToArray(), input.UserDataId);

            return this.RedirectToAction("Create2", "Courier", new { id = imageId, userId = input.UserDataId });
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult AllCouriers()
        {
            var curiers = this.courierService.GetAll<CuriersAll>();
            var viewModel = new AllUserDataCourierViewModel();
            foreach (var curier in curiers)
            {
                var cityName = this.citiesService.GetCityNameByAreaId(curier.AreaId);
                curier.CityName = cityName;
                var courierName = this.usersDataService.GetByUserId<UserDataIndexViewModel>(curier.UserId);
                curier.Name = courierName.Name;
            }

            viewModel.Couriers = curiers;

            return this.View(viewModel);
        }

        [Authorize]
        [Authorize(Roles = "Administrator, Admin")]
        public IActionResult AllRestaurants()
        {
            var reastaurants = this.restaurantService.GetAll<RestaurantAll>();
            var viewModel = new AllUserDataReastaurantViewModel();
            foreach (var reastaurant in reastaurants)
            {
                var cityName = this.citiesService.GetCityNameByAreaId(reastaurant.AreaId);
                reastaurant.CityName = cityName;
                var courierName = this.usersDataService.GetByUserId<UserDataIndexViewModel>(reastaurant.UserId);
                reastaurant.Name = courierName.Name;
            }

            viewModel.Restaurants = reastaurants;

            return this.View(viewModel);
        }
    }
}
