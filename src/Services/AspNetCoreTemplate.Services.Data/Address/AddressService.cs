namespace AspNetCoreTemplate.Services.Data.Address
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
            var address = this.addressRepository.All().Where(x =>
                x.CityId == cityId &&
                x.AreaId == areaId &&
                x.StreetId == streetId &&
                x.LocationId == locationId).FirstOrDefault();

            var cheker = 0;

            if (address == null)
            {
                address = new Address
                {
                    CityId = cityId,
                    AreaId = areaId,
                    StreetId = streetId,
                    LocationId = locationId,
                };
            }
            else
            {
                if (address.CityId != cityId)
                {
                    address = new Address
                    {
                        CityId = cityId,
                        AreaId = areaId,
                        StreetId = streetId,
                        LocationId = locationId,
                    };
                }
                else
                {
                    if (address.AreaId != areaId)
                    {
                        address = new Address
                        {
                            CityId = cityId,
                            AreaId = areaId,
                            StreetId = streetId,
                            LocationId = locationId,
                        };
                    }
                    else
                    {
                        if (address.StreetId != streetId)
                        {
                            address = new Address
                            {
                                CityId = cityId,
                                AreaId = areaId,
                                StreetId = streetId,
                                LocationId = locationId,
                            };
                        }
                        else
                        {
                            if (address.LocationId != locationId)
                            {
                                address = new Address
                                {
                                    CityId = cityId,
                                    AreaId = areaId,
                                    StreetId = streetId,
                                    LocationId = locationId,
                                };
                            }
                            else
                            {
                                cheker = 1;
                            }
                        }
                    }
                }
            }

            if (cheker == 0)
            {
                await this.addressRepository.AddAsync(address);
            }

            await this.addressRepository.SaveChangesAsync();

            return address.Id;
        }

        public async Task<string> CreateAsyncWorkingArea(string areaId, string userId)
        {
            var workingArea = this.workingAreaRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            if (workingArea == null)
            {
                workingArea = new WorkingArea
                {
                    AreaId = areaId,
                    UserId = userId,
                    ActiveWorkingArea = ActiveWorkingArea.No,
                };

                await this.workingAreaRepository.AddAsync(workingArea);
            }

            await this.workingAreaRepository.SaveChangesAsync();

            return workingArea.Id;
        }

        public Address GetAddress(string id)
        {
            var entity = this.addressRepository.All().Where(x => x.Id == id)
                .To<Address>().FirstOrDefault();

            return entity;
        }

        public IEnumerable<T> GetAllAddress<T>(int? count = null)
        {
            IQueryable<Address> query =
               this.addressRepository.All().OrderByDescending(x => x.City.CityName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var entity = this.addressRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return entity;
        }

        public async void ChangeWorkingAreaOff(string id)
        {
            var workinArea = this.workingAreaRepository.All().Where(x => x.Id == id).FirstOrDefault();
            workinArea.ActiveWorkingArea = ActiveWorkingArea.No;

            this.workingAreaRepository.Update(workinArea);
            await this.workingAreaRepository.SaveChangesAsync();
        }

        public async void ChangeWorkingAreaOn(string id, string areaId)
        {
            var workinArea = this.workingAreaRepository.All().Where(x => x.Id == id).FirstOrDefault();
            workinArea.ActiveWorkingArea = ActiveWorkingArea.Yes;
            workinArea.AreaId = areaId;

            this.workingAreaRepository.Update(workinArea);
            await this.workingAreaRepository.SaveChangesAsync();
        }

        public WorkingArea GetByWorkingAreaByUserId(string userId)
        {
            var workingArea = this.workingAreaRepository.All().Where(x => x.UserId == userId).FirstOrDefault();

            return workingArea;
        }
    }
}
