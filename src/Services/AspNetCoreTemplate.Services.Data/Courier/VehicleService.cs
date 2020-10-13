namespace AspNetCoreTemplate.Services.Data.Courier
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Common.Repositories;
    using AspNetCoreTemplate.Data.Models.Couriers;
    using AspNetCoreTemplate.Services.Mapping;

    public class VehicleService : IVehicleService
    {
        private readonly IDeletableEntityRepository<Vehichle> vehicleRepository;

        public VehicleService(IDeletableEntityRepository<Vehichle> vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<string> CreateAsync(string name)
        {
            var vehicle = new Vehichle
            {
                Name = name,
            };

            await this.vehicleRepository.AddAsync(vehicle);

            await this.vehicleRepository.SaveChangesAsync();

            return vehicle.Id;
        }

        public IEnumerable<T> GetAll<T>(int? count = null)
        {
            IQueryable<Vehichle> query =
                this.vehicleRepository.All().OrderBy(x => x.Name);

            if (count.HasValue)
            {
                query = query.Take(count.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetById<T>(string id)
        {
            var vehicle = this.vehicleRepository.All().Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return vehicle;
        }

        public T GetByName<T>(string name)
        {
            var vehicle = this.vehicleRepository.All()
               .Where(x => x.Name.Replace(" ", "-") == name.Replace(" ", "-"))
               .To<T>().FirstOrDefault();

            return vehicle;
        }
    }
}
