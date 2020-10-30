namespace AspNetCoreTemplate.Services.Data.Restaurant
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class RestaurantService : IRestaurantService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;

        public RestaurantService(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Restaurant> restaurantRepository)
        {
            this.userManager = userManager;
            this.cityRepository = cityRepository;
            this.restaurantRepository = restaurantRepository;
        }

        public async Task<string> CreateAsync(string image, string phone, string workingAreaId, string userId, string areaId)
        {
            var restaurant = this.restaurantRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            if (restaurant == null)
            {
                restaurant = new Restaurant
                {
                    Image = image,
                    Phone = phone,
                    WorkingAreaId = workingAreaId,
                    IsWorking = IsWorking.No,
                    UserId = userId,
                    AreaId = areaId,
                };

                await this.restaurantRepository.AddAsync(restaurant);
            }

            await this.restaurantRepository.SaveChangesAsync();

            return restaurant.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Restaurant> query =
               this.restaurantRepository.All().OrderBy(x => x.User.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllNo<T>(int? count = null)
        {
            IQueryable<Restaurant> query =
                this.restaurantRepository.All().Where(x => x.IsRestaurnat == IsRestaurnat.No).OrderBy(x => x.User.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllYes<T>(int? count = null)
        {
            IQueryable<Restaurant> query =
                this.restaurantRepository.All().Where(x => x.IsRestaurnat == IsRestaurnat.Yes).OrderBy(x => x.User.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var restaurant = this.restaurantRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return restaurant;
        }

        public T GetByName<T>(string name)
        {
            var restaurant = this.restaurantRepository.All()
                .Where(x => x.User.UserName.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            return restaurant;
        }

        public T GetByUserId<T>(string id)
        {
            var restaurant = this.restaurantRepository.All().Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return restaurant;
        }

        public async void MadeIsNoRestaurant(string id)
        {
            var restaurant = this.restaurantRepository.All().Where(x => x.Id == id)
                .FirstOrDefault();
            restaurant.IsRestaurnat = IsRestaurnat.Yes;

            this.restaurantRepository.Update(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }

        public async void MadeIsRestaurant(string id)
        {
            var restaurant = this.restaurantRepository.All().Where(x => x.Id == id)
                .FirstOrDefault();
            restaurant.IsRestaurnat = IsRestaurnat.No;

            this.restaurantRepository.Update(restaurant);
            await this.restaurantRepository.SaveChangesAsync();
        }
    }
}
