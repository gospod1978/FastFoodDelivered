namespace AspNetCoreTemplate.Services.Data.Addresses
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class LocationsObjectService : ILocationsObjectService
    {
        private readonly IDeletableEntityRepository<LocationObject> locationRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public LocationsObjectService(
            IDeletableEntityRepository<LocationObject> locationRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.locationRepository = locationRepository;
            this.userManager = userManager;
        }

        public async Task<string> CreateAsyncLocationObject(string name, string adressId, string userId)
        {
            var locationObject = this.locationRepository.All().Where(x =>
                x.Name == name &&
                x.AddressId == adressId &&
                x.UserId == userId).FirstOrDefault();

            var cheker = 0;

            if (locationObject == null)
            {
                locationObject = new LocationObject
                {
                    Name = name,
                    AddressId = adressId,
                    UserId = userId,
                };
            }
            else
            {
                if (locationObject.Name != name)
                {
                    locationObject.Name = name;
                }
                else
                {
                    cheker = 1;
                }
            }

            if (cheker == 0)
            {
                await this.locationRepository.AddAsync(locationObject);
            }

            await this.locationRepository.SaveChangesAsync();

            return locationObject.Id;
        }

        public IEnumerable<T> GetAllByAddressId<T>(string id, int? count = null)
        {
            IQueryable<LocationObject> query =
               this.locationRepository.All().Where(x => x.AddressId == id).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllByUserId<T>(string id, int? count = null)
        {
            IQueryable<LocationObject> query =
               this.locationRepository.All().Where(x => x.UserId == id).OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllLocationsObjects<T>(int? count = null)
        {
            IQueryable<LocationObject> query =
               this.locationRepository.All().OrderByDescending(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.locationRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.locationRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetLocationByUserId<T>(string id, string locationObjName)
        {
            var entity = this.locationRepository.All().Where(x => x.UserId == id && x.Name == locationObjName)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetLocationByUserIdOnly<T>(string id)
        {
            var entity = this.locationRepository.All().Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
