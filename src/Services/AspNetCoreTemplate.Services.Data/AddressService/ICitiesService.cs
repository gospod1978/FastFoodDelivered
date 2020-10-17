namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models.Addresses;

    public interface ICitiesService
    {
        Task<string> CreateAsyncCity(string name);

        IEnumerable<T> GetAllCities<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);
    }
}
