namespace AspNetCoreTemplate.Services.Data.Address
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class CitiesService : ICitiesService
    {
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Area> areaRepository;

        public CitiesService(
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Area> areaRepository)
        {
            this.cityRepository = cityRepository;
            this.areaRepository = areaRepository;
        }

        public async Task<string> CreateAsyncCity(string name)
        {
            var city = this.cityRepository.All().Where(x => x.CityName == name.ToUpper()).FirstOrDefault();
            if (city == null)
            {
                city = new City
                {
                    CityName = name.ToUpper(),
                };
                await this.cityRepository.AddAsync(city);

                await this.cityRepository.SaveChangesAsync();
            }

            return city.Id;
        }

        public IEnumerable<T> GetAllCities<T>(int? count = null)
        {
            IQueryable<City> query =
                this.cityRepository.All().OrderByDescending(x => x.Areas.Count());

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var city = this.cityRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return city;
        }

        public T GetByName<T>(string name)
        {
            var city = this.cityRepository.All().Where(x => x.CityName == name)
                .To<T>().FirstOrDefault();

            return city;
        }

        public string GetCityNameByAreaId(string id)
        {
            var cityData = this.areaRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var cityName = this.cityRepository.All().Where(x => x.Id == cityData.CityId).FirstOrDefault();

            return cityName.CityName;
        }
    }
}
