namespace AspNetCoreTemplate.Services.Data.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICitiesService
    {
        Task<string> CreateAsyncCity(string name);

        IEnumerable<T> GetAllCities<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        string GetCityNameByAreaId(string id);
    }
}
