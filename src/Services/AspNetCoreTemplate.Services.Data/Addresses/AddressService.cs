namespace AspNetCoreTemplate.Services.Data.Addresses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;

    public class AddressService : IAddressService
    {
        private readonly IDeletableEntityRepository<Address> addressRepository;
        private readonly IDeletableEntityRepository<WorkingArea> workingAreaRepository;

        public AddressService(
            IDeletableEntityRepository<Address> addressRepository,
            IDeletableEntityRepository<WorkingArea> workingAreaRepository)
        {
            this.addressRepository = addressRepository;
            this.workingAreaRepository = workingAreaRepository;
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

        public IndexAddressViewModel GetAddress(string id)
        {
            var entity = this.addressRepository.All().Where(x => x.Id == id)
                .To<IndexAddressViewModel>().FirstOrDefault();

            return entity;
        }

        public IEnumerable<T> GetAllAddress<T>(int? count = null)
        {
            IQueryable<Address> query =
               this.addressRepository.All().OrderByDescending(x => x.Id);

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
