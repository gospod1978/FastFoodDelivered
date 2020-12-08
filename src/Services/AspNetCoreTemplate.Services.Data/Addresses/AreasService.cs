namespace AspNetCoreTemplate.Services.Data.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class AreasService : IAreasService
    {
        private readonly IDeletableEntityRepository<Area> areaRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;

        public AreasService(
            IDeletableEntityRepository<Area> areaRepository,
            IDeletableEntityRepository<City> cityRepository)
        {
            this.areaRepository = areaRepository;
            this.cityRepository = cityRepository;
        }

        public async Task<string> CreateAsyncArea(string name, string cityId, string image)
        {
            var area = this.areaRepository.All().Where(x => x.AreaName == name).FirstOrDefault();
            if (area == null)
            {
                area = new Area
                {
                    AreaName = name,
                    CityId = cityId,
                    Image = image,
                };

                await this.areaRepository.AddAsync(area);

                await this.areaRepository.SaveChangesAsync();
            }

            return area.Id;
        }

        public IEnumerable<T> GetAllAreas<T>(string id, int? count = null)
        {
            IQueryable<Area> query =
               this.areaRepository.All().Where(a => a.CityId == id).OrderByDescending(x => x.Streets.Count());

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllWorkingAreas<T>(string id, int? count = null)
        {
            var areas = this.areaRepository.All().Where(x => x.CityId == id);

            IQueryable<Area> query = areas.OrderBy(x => x.AreaName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.areaRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByName<T>(string name)
        {
            var entity = this.areaRepository.All().Where(x => x.AreaName == name)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetCityByAreaId<T>(string id)
        {
            var entity = this.areaRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
