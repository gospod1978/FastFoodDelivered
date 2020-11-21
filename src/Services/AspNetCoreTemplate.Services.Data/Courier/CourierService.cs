namespace AspNetCoreTemplate.Services.Data.Courier
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models;
    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Data.AddressService;
    using AspNetCoreTemplate.Services.Mapping;
    using AspNetCoreTemplate.Web.ViewModels.Couriers;
    using Microsoft.AspNetCore.Identity;

    public class CourierService : ICourierService
    {
        private readonly IDeletableEntityRepository<Courier> courierRepository;
        private readonly IDeletableEntityRepository<Vehichle> vehichleRepository;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<WorkingArea> workingAreaRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAddressService addressService;

        public CourierService(
            IDeletableEntityRepository<Courier> courierRepository,
            IDeletableEntityRepository<Vehichle> vehichleRepository,
            IDeletableEntityRepository<City> cityRepository,
            IDeletableEntityRepository<WorkingArea> workingAreaRepository,
            UserManager<ApplicationUser> userManager,
            IAddressService addressService)
        {
            this.courierRepository = courierRepository;
            this.vehichleRepository = vehichleRepository;
            this.cityRepository = cityRepository;
            this.workingAreaRepository = workingAreaRepository;
            this.userManager = userManager;
            this.addressService = addressService;
        }

        public async Task<string> CreateAsync(string image, string phone, string vechileID, DateTime birthday, string workingAreaId, string userId, string areaId)
        {
            var courier = this.courierRepository.All().Where(x => x.UserId == userId).FirstOrDefault();
            var vehcile = this.vehichleRepository.All().Where(x => x.Id == vechileID).FirstOrDefault();

            if (courier == null)
            {
                courier = new Courier
                {
                    Image = image,
                    Phone = phone,
                    Vehicle = vehcile,
                    WorkingAreaId = workingAreaId,
                    IsWorking = IsWorking.No,
                    IsCourier = IsCourier.No,
                    UserId = userId,
                    Birthday = birthday,
                    AreaId = areaId,
                };

                await this.courierRepository.AddAsync(courier);
            }

            await this.courierRepository.SaveChangesAsync();

            return courier.Id;
        }

        public async Task<string> CreateWorkingAreaByCourierId(string courierId, string areaId)
        {
            var courier = this.courierRepository.All().Where(x => x.Id == courierId).FirstOrDefault();
            var workingArea = this.addressService.GetByWorkingAreaByUserId(courier.UserId);
            workingArea.AreaId = areaId;
            if (workingArea.ActiveWorkingArea == ActiveWorkingArea.Yes)
            {
                workingArea.ActiveWorkingArea = ActiveWorkingArea.No;
            }
            else
            {
                workingArea.ActiveWorkingArea = ActiveWorkingArea.Yes;
            }

            courier.IsWorking = IsWorking.Yes;
            this.courierRepository.Update(courier);
            this.workingAreaRepository.Update(workingArea);
            await this.workingAreaRepository.SaveChangesAsync();
            await this.courierRepository.SaveChangesAsync();

            return courier.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Courier> query =
                this.courierRepository.All().OrderBy(x => x.User.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllNo<T>(int? count = null)
        {
            IQueryable<Courier> query =
                this.courierRepository.All().Where(x => x.IsCourier == IsCourier.No).OrderBy(x => x.User.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllYes<T>(int? count = null)
        {
            IQueryable<Courier> query =
                this.courierRepository.All().Where(x => x.IsCourier == IsCourier.Yes).OrderBy(x => x.User.UserName);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var courier = this.courierRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return courier;
        }

        public T GetByName<T>(string name)
        {
            var courier = this.courierRepository.All()
                .Where(x => x.User.UserName.Replace(" ", "-") == name.Replace(" ", "-"))
                .To<T>().FirstOrDefault();

            return courier;
        }

        public T GetByUserId<T>(string id)
        {
            var courier = this.courierRepository.All().Where(x => x.UserId == id)
                .To<T>().FirstOrDefault();

            return courier;
        }

        public async void MadeIsCourier(string id)
        {
            var courier = this.courierRepository.All().Where(x => x.Id == id)
                .FirstOrDefault();
            courier.IsCourier = IsCourier.Yes;

            this.courierRepository.Update(courier);
            await this.courierRepository.SaveChangesAsync();
        }

        public async void MadeIsNoCourier(string id)
        {
            var courier = this.courierRepository.All().Where(x => x.Id == id)
               .FirstOrDefault();
            courier.IsCourier = IsCourier.No;

            this.courierRepository.Update(courier);
            await this.courierRepository.SaveChangesAsync();
        }
    }
}
