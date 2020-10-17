namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;

    public class LocationsService : ILocationsService
    {
        private readonly IDeletableEntityRepository<Street> streetRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;

        public LocationsService(
            IDeletableEntityRepository<Street> streetRepository,
            IDeletableEntityRepository<Location> locationRepository)
        {
            this.streetRepository = streetRepository;
            this.locationRepository = locationRepository;
        }

        public async Task<string> CreateAsyncLocation(int apartment, int number, int flour, int entrance, string streetId)
        {
            var location = this.locationRepository.All().Where(x => x.StreetId == streetId).FirstOrDefault();
            if (location == null)
            {
                location = new Location
                {
                    Apartament = apartment,
                    Number = number,
                    Flour = flour,
                    Entrance = entrance,
                    StreetId = streetId,
                };
            }
            else
            {
                if (location.Number != number)
                {
                    location = new Location
                    {
                        Apartament = apartment,
                        Number = number,
                        Flour = flour,
                        Entrance = entrance,
                        StreetId = streetId,
                    };
                }
                else
                {
                    if (location.Entrance != entrance)
                    {
                        location = new Location
                        {
                            Apartament = apartment,
                            Number = number,
                            Flour = flour,
                            Entrance = entrance,
                            StreetId = streetId,
                        };
                    }
                    else
                    {
                        if (location.Flour != flour)
                        {
                            location = new Location
                            {
                                Apartament = apartment,
                                Number = number,
                                Flour = flour,
                                Entrance = entrance,
                                StreetId = streetId,
                            };
                        }
                        else
                        {
                            if (location.Apartament != apartment)
                            {
                                location = new Location
                                {
                                    Apartament = apartment,
                                    Number = number,
                                    Flour = flour,
                                    Entrance = entrance,
                                    StreetId = streetId,
                                };
                            }
                        }
                    }
                }
            }

            await this.locationRepository.AddAsync(location);

            await this.locationRepository.SaveChangesAsync();

            return location.Id;
        }

        public IEnumerable<T> GetAllLocations<T>(string id, int? count = null)
        {
            IQueryable<Location> query =
               this.locationRepository.All().Where(a => a.StreetId == id).OrderByDescending(x => x.Apartament);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByApartament<T>(int apartament)
        {
            var entity = this.locationRepository.All().Where(x => x.Apartament == apartament)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByEntrance<T>(int entrance)
        {
            var entity = this.locationRepository.All().Where(x => x.Entrance == entrance)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByFlour<T>(int flour)
        {
            var entity = this.locationRepository.All().Where(x => x.Flour == flour)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetById<T>(string id)
        {
            var entity = this.locationRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByNumber<T>(int number)
        {
            var entity = this.locationRepository.All().Where(x => x.Number == number)
                .To<T>().FirstOrDefault();

            return entity;
        }
    }
}
