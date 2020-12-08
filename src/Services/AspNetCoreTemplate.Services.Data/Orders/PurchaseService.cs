namespace AspNetCoreTemplate.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Common;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Data.Courier;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;

    public class PurchaseService : IPurchaseService
    {
        private readonly IDeletableEntityRepository<Purchase> purchaseRepository;
        private readonly IDeletableEntityRepository<Courier> courierRepository;
        private readonly IDeletableEntityRepository<Area> areaRepository;
        private readonly IDeletableEntityRepository<WorkingArea> workingAreaRepostiory;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepostiory;
        private readonly ICourierService courierService;

        public PurchaseService(
            IDeletableEntityRepository<Purchase> purchaseRepository,
            IDeletableEntityRepository<Courier> courierRepository,
            IDeletableEntityRepository<Area> areaRepository,
            IDeletableEntityRepository<WorkingArea> workingAreaRepostiory,
            IDeletableEntityRepository<Restaurant> restaurantRepostiory,
            ICourierService courierService)
        {
            this.purchaseRepository = purchaseRepository;
            this.courierRepository = courierRepository;
            this.areaRepository = areaRepository;
            this.workingAreaRepostiory = workingAreaRepostiory;
            this.restaurantRepostiory = restaurantRepostiory;
            this.courierService = courierService;
        }

        public string AddCourier(string purchazeId, string courierId)
        {
            var enitity = this.purchaseRepository.All().Where(x => x.Id == purchazeId).FirstOrDefault();
            enitity.CourierId = courierId;
            this.purchaseRepository.Update(enitity);
            this.purchaseRepository.SaveChangesAsync();

            return enitity.Id;
        }

        public async Task<string> ChangeStatus(string purchazeId, string status)
        {
            var enitity = this.purchaseRepository.All().Where(x => x.Id == purchazeId).FirstOrDefault();

            var newStatus = (Status)Enum.Parse(typeof(Status), status);

            enitity.Status = newStatus;

            this.purchaseRepository.Update(enitity);
            await this.purchaseRepository.SaveChangesAsync();

            return enitity.Id;
        }

        public string CourierIdFind(string restAreaId)
        {
            var allCourier = this.courierService.GetAllYes<CourierDetailsViewModel>();
            string courierId = string.Empty;

            foreach (var courier in allCourier)
            {
                var areaWorking = this.workingAreaRepostiory.All().Where(x => x.AreaId == restAreaId).Select(x => new
                {
                    Id = x.Id,
                    AreaId = x.AreaId,
                });
                foreach (var area in areaWorking)
                {
                    if (courier.WorkingAreaId == area.Id)
                    {
                        courierId = courier.Id;
                    }
                    else
                    {
                        courierId = courier.Id;
                    }
                }
            }

            return courierId;
        }

        public async Task<string> CreateAsyncMenu(string orderId, string userId, string? courierId, string restaurantID, string promotionType, decimal menuPrice, decimal deliveryPrice)
        {
            var newPromotionType = (PromotionType)Enum.Parse(typeof(PromotionType), promotionType);
            var menu = new Purchase
            {
                OrderId = orderId,
                UserId = userId,
                CourierId = courierId,
                RestaurantId = restaurantID,
                Status = Status.Purchaise,
                PromotionType = newPromotionType,
                Price = menuPrice + deliveryPrice,
                MenuPrice = menuPrice,
                DeliveryPrice = deliveryPrice,
            };

            await this.purchaseRepository.AddAsync(menu);
            await this.purchaseRepository.SaveChangesAsync();

            return menu.Id;
        }

        public Dictionary<string, decimal> FindCourier(string restaurantLocation)
        {
            var courierPrice = new Dictionary<string, decimal>();
            var courier = this.courierRepository.All().Where(x => x.AreaId == restaurantLocation).FirstOrDefault();

            var area = this.areaRepository.All().Where(x => x.Id == restaurantLocation).FirstOrDefault();
            var areas = this.AreaLocationSofia();

            var areaRest = areas.FirstOrDefault(x => x.Key == area.AreaName);
            string courierId = string.Empty;
            decimal deliveryPrice = 0M;

            if (courier == null)
            {
                var result = this.ReturnCourier(areaRest.Value);
                courierId = result.ElementAt(0).Key;
                deliveryPrice = result.ElementAt(0).Value;
                courierPrice.Add(courierId, deliveryPrice);
            }

            return courierPrice;
        }

        public IEnumerable<T> GetAllByCourierId<T>(string id, int? count = null)
        {
            IQueryable<Purchase> query =
            this.purchaseRepository.All().Where(a => a.CourierId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAll<T>(string id, string role, int? days = null, int? count = null)
        {
            if (days != null)
            {
                var data = DateTime.Today;
                if (days == 7)
                {
                    data = DateTime.Today.AddDays(-7);
                }
                else if (days == 30)
                {
                    data = DateTime.Today.AddDays(-30);
                }

                if (role == GlobalConstants.AdminRoleName || role == GlobalConstants.AdministratorRoleName)
                {
                    IQueryable<Purchase> query =
                this.purchaseRepository.All().Where(o => o.CreatedOn >= data).OrderByDescending(x => x.Status);

                    if (count.HasValue)
                    {
                        query = query.Take(count.Value);
                    }

                    return query.To<T>().ToList();
                }
                else if (role == GlobalConstants.CourierRoleName)
                {
                    var courier = this.courierRepository.All().Where(x => x.UserId == id).FirstOrDefault();
                    IQueryable<Purchase> query =
                this.purchaseRepository.All().Where(x => x.CourierId == courier.Id).OrderByDescending(x => x.Status);
                    query = query.Where(o => o.CreatedOn >= data);
                    if (count.HasValue)
                    {
                        query = query.Take(count.Value);
                    }

                    return query.To<T>().ToList();
                }
                else
                {
                    var restaurant = this.restaurantRepostiory.All().Where(x => x.UserId == id).FirstOrDefault();
                    IQueryable<Purchase> query =
                this.purchaseRepository.All().Where(x => x.RestaurantId == restaurant.Id).OrderByDescending(x => x.Status);
                    query = query.Where(o => o.CreatedOn >= data);
                    if (count.HasValue)
                    {
                        query = query.Take(count.Value);
                    }

                    return query.To<T>().ToList();
                }
            }
            else
            {
                if (role == GlobalConstants.AdminRoleName || role == GlobalConstants.AdministratorRoleName)
                {
                    IQueryable<Purchase> query =
                this.purchaseRepository.All().OrderByDescending(x => x.Status);

                    if (count.HasValue)
                    {
                        query = query.Take(count.Value);
                    }

                    return query.To<T>().ToList();
                }
                else if (role == GlobalConstants.CourierRoleName)
                {
                    var courier = this.courierRepository.All().Where(x => x.UserId == id).FirstOrDefault();
                    IQueryable<Purchase> query =
                this.purchaseRepository.All().Where(x => x.CourierId == courier.Id).OrderByDescending(x => x.Status);

                    if (count.HasValue)
                    {
                        query = query.Take(count.Value);
                    }

                    return query.To<T>().ToList();
                }
                else
                {
                    var restaurant = this.restaurantRepostiory.All().Where(x => x.UserId == id).FirstOrDefault();
                    IQueryable<Purchase> query =
                this.purchaseRepository.All().Where(x => x.RestaurantId == restaurant.Id).OrderByDescending(x => x.Status);

                    if (count.HasValue)
                    {
                        query = query.Take(count.Value);
                    }

                    return query.To<T>().ToList();
                }
            }
        }

        public IEnumerable<T> GetAllByPromotionType<T>(string promotionType, int? count = null)
        {
            var newPromotionType = (PromotionType)Enum.Parse(typeof(PromotionType), promotionType);
            IQueryable<Purchase> query =
             this.purchaseRepository.All().Where(a => a.PromotionType == newPromotionType).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByRestaurantId<T>(string id, int? count = null)
        {
            IQueryable<Purchase> query =
            this.purchaseRepository.All().Where(a => a.RestaurantId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByStatus<T>(string status, int? count = null)
        {
            var newStatus = (Status)Enum.Parse(typeof(Status), status);
            IQueryable<Purchase> query =
            this.purchaseRepository.All().Where(a => a.Status == newStatus).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int? count = null)
        {
            IQueryable<Purchase> query =
            this.purchaseRepository.All().Where(a => a.UserId == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var purchase = this.purchaseRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return purchase;
        }

        public decimal PriceCourier(string userAreaName, string restAreaName)
        {
            var area = this.AreaLocationSofia();
            var userPostCode = 0;
            var restPostCode = 0;
            var deliveryPrice = 0M;

            if (area.ContainsKey(userAreaName))
            {
                userPostCode = area[userAreaName];
            }

            if (area.ContainsKey(restAreaName))
            {
                restPostCode = area[restAreaName];
            }

            var result = Math.Abs(userPostCode - restPostCode);

            if (result < 10)
            {
                deliveryPrice = 3.90M;
            }
            else if (result >= 10 && result < 20)
            {
                deliveryPrice = 5.90M;
            }
            else if (result >= 20 && result < 30)
            {
                deliveryPrice = 7.90M;
            }
            else if (result >= 30 && result < 40)
            {
                deliveryPrice = 9.90M;
            }
            else
            {
                deliveryPrice = 11.90M;
            }

            return deliveryPrice;
        }

        private Dictionary<string, int> AreaLocationSofia()
        {
            var areas = new Dictionary<string, int>();
            areas.Add("Sredec", 11);
            areas.Add("KrasnoSelo", 12);
            areas.Add("Vazrajdane", 13);
            areas.Add("Oborishte", 14);
            areas.Add("Serdika", 15);
            areas.Add("Poduene", 16);
            areas.Add("Slatina", 21);
            areas.Add("Izgrev", 22);
            areas.Add("Lozenetz", 23);
            areas.Add("Triadica", 24);
            areas.Add("KrasnaPolqna", 25);
            areas.Add("Ilinden", 26);
            areas.Add("Nadejda", 31);
            areas.Add("Iskar", 32);
            areas.Add("Mladost", 33);
            areas.Add("Studentski", 34);
            areas.Add("Vitosha", 35);
            areas.Add("OvchaKupel", 36);
            areas.Add("Lulin", 41);
            areas.Add("Vrabnica", 42);
            areas.Add("NoviIskar", 43);
            areas.Add("Kremikovci", 44);
            areas.Add("Pancharevo", 45);
            areas.Add("Bankq", 46);

            return areas;
        }

        private Dictionary<string, decimal> ReturnCourier(int area)
        {
            var areas = this.AreaLocationSofia();
            var postOne = area % 10;
            var postTwo = postOne % 10;
            var restArea = postOne + postTwo;
            var result = 0;
            var info = new Dictionary<string, decimal>();
            var deliveryPrice = 0M;

            foreach (var areaClose in areas)
            {
                var one = areaClose.Value % 10;
                var two = one % 10;
                var distans = one + two;

                if (distans > restArea)
                {
                    result = distans - restArea;
                }
                else if (distans < restArea)
                {
                    result = restArea - distans;
                }

                if (result == 1)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 3.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 2)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 4.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 3)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 5.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 4)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 6.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 5)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 7.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 6)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 8.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 7)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 9.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
                else if (result == 8)
                {
                    var areaName = areaClose.Key;
                    var areaId = this.areaRepository.All().Where(x => x.AreaName == areaName).FirstOrDefault();
                    var courier = this.courierRepository.All().Where(x => x.AreaId == areaId.Id).FirstOrDefault();
                    if (courier.Id != null)
                    {
                        deliveryPrice = 1 * 10.90M;
                        info.Add(courier.Id, deliveryPrice);
                        return info;
                    }
                }
            }

            return null;
        }
    }
}
