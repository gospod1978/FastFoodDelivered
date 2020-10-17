namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStreetsService
    {
        Task<string> CreateAsyncStreet(string name, string areaId);

        IEnumerable<T> GetAllStreets<T>(string id, int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);
    }
}
