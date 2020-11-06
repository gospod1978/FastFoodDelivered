﻿namespace AspNetCoreTemplate.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Orders;
    using AspNetCoreTemplate.Services.Mapping;

    public class PurchaseService : IPurchaseService
    {
        private readonly IDeletableEntityRepository<Purchase> purchaseRepository;
        private readonly IDeletableEntityRepository<Courier> courierRepository;
        private readonly IDeletableEntityRepository<Area> areaRepository;

        public PurchaseService(
            IDeletableEntityRepository<Purchase> purchaseRepository,
            IDeletableEntityRepository<Courier> courierRepository,
            IDeletableEntityRepository<Area> areaRepository)
        {
            this.purchaseRepository = purchaseRepository;
            this.courierRepository = courierRepository;
            this.areaRepository = areaRepository;
        }

        public string AddCourier(string purchazeId, string courierId)
        {
            var enitity = this.purchaseRepository.All().Where(x => x.Id == purchazeId).FirstOrDefault();
            enitity.CourierId = courierId;
            this.purchaseRepository.Update(enitity);
            this.purchaseRepository.SaveChangesAsync();

            return enitity.Id;
        }

        public string ChangeStatus(string purchazeId, string userId, string status)
        {
            var enitity = this.purchaseRepository.All().Where(x => x.Id == purchazeId && x.UserId == userId).FirstOrDefault();

            var newStatus = (Status)Enum.Parse(typeof(Status), status);

            enitity.Status = newStatus;
            this.purchaseRepository.Update(enitity);
            this.purchaseRepository.SaveChangesAsync();

            return enitity.Id;
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

        public IEnumerable<T> GetAllById<T>(string id, int? count = null)
        {
            IQueryable<Purchase> query =
            this.purchaseRepository.All().Where(a => a.Id == id).OrderByDescending(x => x.Price);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
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
