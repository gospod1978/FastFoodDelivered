namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Data.Models.Restaurants;
    using AspNetCoreTemplate.Services.Mapping;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Area> areaRepository;
        private readonly IDeletableEntityRepository<Street> streetRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;
        private readonly IDeletableEntityRepository<LocationObject> locationObjectRepository;
        private readonly IDeletableEntityRepository<WorkingArea> workingAreaRepository;
        private readonly IDeletableEntityRepository<Courier> courierManager;
        private readonly IDeletableEntityRepository<Restaurant> restaurantManager;

        public AddressService(
            IDeletableEntityRepository<Address> addressRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<Area> areaRepository,
            IDeletableEntityRepository<Street> streetRepository,
            IDeletableEntityRepository<Location> locationRepository,
            IDeletableEntityRepository<LocationObject> locationObjectRepository,
            IDeletableEntityRepository<WorkingArea> workingAreaRepository,
            IDeletableEntityRepository<Courier> courierManager,
            IDeletableEntityRepository<Restaurant> restaurantManager)
        {
            this.addressRepository = addressRepository;
            this.cityRepository = cityRepository;
            this.areaRepository = areaRepository;
            this.streetRepository = streetRepository;
            this.locationRepository = locationRepository;
            this.locationObjectRepository = locationObjectRepository;
            this.workingAreaRepository = workingAreaRepository;
            this.courierManager = courierManager;
            this.restaurantManager = restaurantManager;
        }

        public async Task<string> CreateAsyncAddress(string cityId, string areaId, string streetId, string locationId)
        {
            var address = new Address
            {
                CityId = cityId,
                AreaId = areaId,
                StreetId = streetId,
                LocationId = locationId,
            };

            await this.addressRepository.AddAsync(address);

            await this.addressRepository.SaveChangesAsync();

            return address.Id;
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

        public async Task<string> CreateAsyncArea(string name, string cityId, string image)
        {
            //var areaName = Enum.Parse<AreaName>(name);
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

        public async Task<string> CreateAsyncStreet(string name, string areaId)
        {
            var street = this.streetRepository.All().Where(x => x.Name == name).FirstOrDefault();
            if (street == null)
            {
                street = new Street
                {
                    Name = name,
                    AreaId = areaId,
                };

                await this.streetRepository.AddAsync(street);

                await this.streetRepository.SaveChangesAsync();
            }

            return street.Id;
        }

        public async Task<string> CreateAsyncLocation(int apartment, int number, int flour, int entrance, string streetId)
        {
            var location = this.locationRepository.All().Select(x => new Location
            {
                StreetId = streetId,
                Entrance = entrance,
            }).FirstOrDefault();

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

                await this.locationRepository.AddAsync(location);

                await this.locationRepository.SaveChangesAsync();
            }
            else
            {
                var locFlour = this.locationRepository.All().Select(x => new Location
                {
                    StreetId = streetId,
                    Entrance = entrance,
                    Flour = flour,
                }).FirstOrDefault();

                if (locFlour == null)
                {
                    location.Flour = flour;
                    location.Number = number;
                    location.Apartament = apartment;

                    await this.locationRepository.AddAsync(location);

                    await this.locationRepository.SaveChangesAsync();
                }
                else
                {
                    var locNumber = this.locationRepository.All().Select(x => new Location
                    {
                        StreetId = streetId,
                        Entrance = entrance,
                        Flour = flour,
                        Number = number,
                    }).FirstOrDefault();

                    if (locNumber == null)
                    {
                        location.Number = number;
                        location.Apartament = apartment;

                        await this.locationRepository.AddAsync(location);

                        await this.locationRepository.SaveChangesAsync();
                    }
                    else
                    {
                        var locApartment = this.locationRepository.All().Select(x => new Location
                        {
                            StreetId = streetId,
                            Entrance = entrance,
                            Flour = flour,
                            Number = number,
                            Apartament = apartment,
                        }).FirstOrDefault();

                        if (locApartment == null)
                        {
                            location.Apartament = apartment;

                            await this.locationRepository.AddAsync(location);

                            await this.locationRepository.SaveChangesAsync();
                        }
                    }
                }
            }

            return location.Id;
        }

        public async Task<string> CreateAsyncLocationObject(string name, string adressId)
        {
            var locationObject = new LocationObject
            {
                Name = name,
                AddressId = adressId,
            };

            await this.locationObjectRepository.AddAsync(locationObject);

            await this.locationObjectRepository.SaveChangesAsync();

            return locationObject.Id;
        }

        public async Task<string> CreateAsyncWorkingAreaCourier(string userId, string areaId)
        {
            var courier = this.courierManager.All().Where(x => x.UserId == userId).FirstOrDefault();
            var workingArea = new WorkingArea
            {
                AreaId = areaId,
            };

            courier.WorkingArea.AreaId = workingArea.AreaId;

            await this.workingAreaRepository.AddAsync(workingArea);

            await this.workingAreaRepository.SaveChangesAsync();

            await this.courierManager.SaveChangesAsync();

            return workingArea.Id;
        }

        public async Task<string> CreateAsyncWorkingAreaRestaurant(string userId, string areaId)
        {
            var restaurant = this.restaurantManager.All().Where(x => x.UserId == userId).FirstOrDefault();
            var workingArea = new WorkingArea
            {
                AreaId = areaId,
            };

            restaurant.WorkingArea.AreaId = workingArea.AreaId;

            await this.workingAreaRepository.AddAsync(workingArea);

            await this.workingAreaRepository.SaveChangesAsync();

            await this.restaurantManager.SaveChangesAsync();

            return workingArea.Id;
        }

        public IEnumerable<T> GetAllAddress<T>(int? count = null)
        {
            IQueryable<Address> query =
                this.addressRepository.All().OrderBy(x => x.City);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
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

        public IEnumerable<T> GetAllAreas<T>(string id, int? count = null)
        {
            var city = this.cityRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var areas = this.areaRepository.All().Where(x => x.CityId == city.Id);

            IQueryable<Area> query = areas.OrderBy(x => x.AreaName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllStreets<T>(string id, int? count = null)
        {
            var area = this.areaRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var streets = this.streetRepository.All().Where(x => x.AreaId == area.Id);

            IQueryable<Street> query = streets.OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllLocations<T>(string id, int? count = null)
        {
            var street = this.streetRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var locations = this.locationRepository.All().Where(x => x.StreetId == street.Id);

            IQueryable<Location> query = locations.OrderBy(x => x.Apartament);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllLocationObjects<T>(string id, int? count = null)
        {
            var address = this.addressRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var locObjects = this.locationObjectRepository.All().Where(x => x.AddressId == address.Id);

            IQueryable<LocationObject> query = locObjects.OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllWorkingAreas<T>(string id, int? count = null)
        {
            var area = this.areaRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var workingAreas = this.workingAreaRepository.All().Where(x => x.AreaId == area.Id);

            IQueryable<WorkingArea> query = workingAreas.OrderBy(x => x.Area);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByNameCity<T>(string name)
        {
            // var city = this.cityRepository.All()
            //    .Where(x => x.CityName == name)
            //    .To<T>().FirstOrDefault();
            IQueryable<City> cities = this.cityRepository.All();
            var citye = cities.Where(x => x.CityName == name);

            return (T)citye.To<T>();
        }

        public T GetByIdCity<T>(string id)
        {
            var entity = this.cityRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public T GetByNameArea<T>(string name)
        {
            //var areaName = Enum.Parse<AreaName>(name);
            var area = this.areaRepository.All()
                .Where(x => x.AreaName == name)
                .To<T>().FirstOrDefault();

            return area;
        }

        public T GetByNameStreets<T>(string name)
        {
            var street = this.streetRepository.All()
                .Where(x => x.Name == name)
                .To<T>().FirstOrDefault();

            return street;
        }

        public T GetByNameLocations<T>(int number)
        {
            var location = this.locationRepository.All()
                .Where(x => x.Number == number)
                .To<T>().FirstOrDefault();

            return location;
        }

        public T GetByName<T>(string name)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(string id)
        {
            throw new NotImplementedException();
        }
    }
}
