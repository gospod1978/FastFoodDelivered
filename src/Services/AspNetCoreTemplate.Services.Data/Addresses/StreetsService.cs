namespace AspNetCoreTemplate.Services.Data.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class StreetsService : IStreetsService
    {
        private readonly IDeletableEntityRepository<Street> streetRepository;

        public StreetsService(IDeletableEntityRepository<Street> streetRepository)
        {
            this.streetRepository = streetRepository;
        }

        public async Task<string> CreateAsyncStreet(string name, string areaId)
        {
            var street = this.streetRepository.All().Where(x => x.Name == name && x.AreaId == areaId).FirstOrDefault();
            if (street == null)
            {
                street = new Street
                {
                    Name = name,
                    AreaId = areaId,
                };
            }

            await this.streetRepository.AddAsync(street);

            await this.streetRepository.SaveChangesAsync();

            return street.Id;
        }

        public IEnumerable<T> GetAllStreets<T>(string id, int? count = null)
        {
            IQueryable<Street> query =
               this.streetRepository.All().Where(a => a.AreaId == id).OrderByDescending(x => x.Locations.Count());

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.streetRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.streetRepository.All().Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
