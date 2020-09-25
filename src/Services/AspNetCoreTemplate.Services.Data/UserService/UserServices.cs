namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRoleServices roleServices;

        public UserServices(
            UserManager<ApplicationUser> userManager,
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            RoleManager<ApplicationRole> roleManager,
            IRoleServices roleServices)
        {
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.roleManager = roleManager;
            this.roleServices = roleServices;
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
            var users = this.userManager.Users.Where(x => x.Id == id)
                .To<T>().FirstOrDefault();
            return users;
        }

        public T GetByName<T>(string name)
        {
            var user = this.userManager.Users
                .Where(x => x.UserName.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            return user;
        }
    }
}
