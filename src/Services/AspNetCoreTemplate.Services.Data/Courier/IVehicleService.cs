namespace AspNetCoreTemplate.Services.Data.Courier
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVehicleService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        Task<string> CreateAsync(string name);
    }
}
