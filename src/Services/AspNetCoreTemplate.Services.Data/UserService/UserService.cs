namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Data.Models.UserHome;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRoleService roleServices;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<UserData> dataRepository;
        private readonly IDeletableEntityRepository<Restaurant> restaurantRepository;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IRoleService roleServices,
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IDeletableEntityRepository<UserData> dataRepository,
            IDeletableEntityRepository<Restaurant> restaurantRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.roleServices = roleServices;
            this.userRepository = userRepository;
            this.dataRepository = dataRepository;
            this.restaurantRepository = restaurantRepository;
        }

        public async void AddRoleCourier(string userName, string role)
        {
            var user = this.userManager.Users.Where(x => x.UserName == userName).
                FirstOrDefault();
            await this.userManager.AddToRoleAsync(user, role);
        }

        public async Task<string> AddRoleToUser<T>(string userName, string roleName)
        {
            var user = this.userManager.Users.Where(x => x.UserName == userName).
                FirstOrDefault();
            var role = this.roleManager.Roles.Where(x => x.Name == roleName)
                .FirstOrDefault();

            await this.userManager.AddToRoleAsync(user, role.Name);

            return user.UserName.ToString();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationUser> query =
                this.userManager.Users.OrderBy(x => x.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var user = this.userManager.Users.Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return user;
        }

        public T GetByName<T>(string name)
        {
            var user = this.userManager.Users
                .Where(x => x.UserName.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            return user;
        }

        public T GetByUserByUserDataId<T>(string id)
        {
            var user = this.dataRepository.All().Where(x => x.Id == id).Select(u => u.UserId)
                .To<T>().FirstOrDefault();

            return user;
        }

        public string GetUserByRestaurantId(string id)
        {
            var user = this.restaurantRepository.All().Where(x => x.Id == id).FirstOrDefault();

            return user.Id;
        }

        public string GetUserName(string name)
        {
            var user = this.userRepository.All().Where(x => x.UserName == name).FirstOrDefault();

            return user.Id;
        }
    }
}
