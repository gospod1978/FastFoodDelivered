namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class RoleServices : IRoleServices
    {
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;
        private readonly RoleManager<ApplicationRole> roleManager;

        public RoleServices(
            IDeletableEntityRepository<ApplicationRole> roleRepository,
            RoleManager<ApplicationRole> roleManager)
        {
            this.roleRepository = roleRepository;
            this.roleManager = roleManager;
        }

        public async Task<string> CreateAsyncRole(string name)
        {
            var role = new ApplicationRole
            {
                Name = name,
                NormalizedName = name.ToUpper(),
            };

            await this.roleRepository.AddAsync(role);
            await this.roleRepository.SaveChangesAsync();

            return role.Id;
        }

        public async Task DeleteByName(string name)
        {
            var role = this.roleManager.Roles.FirstOrDefault(x => x.Name == name);
            this.roleRepository.Delete(role);
            await this.roleRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<ApplicationRole> query =
                this.roleRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public string GetById<T>(string id)
        {
            var role = this.roleManager.Roles.Where(x => x.Id == id).FirstOrDefault();

            return role.Name;
        }

        public async Task DeleteById(string id)
        {
            var role = this.roleManager.Roles.Where(x => x.Id == id).FirstOrDefault();
            this.roleRepository.Delete(role);
            await this.roleRepository.SaveChangesAsync();
        }
    }
}
