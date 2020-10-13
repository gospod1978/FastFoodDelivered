namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressService
    {
        Task<string> CreateAsyncAddress(string cityId, string areaId, string streetId, string locationId);

        Task<string> CreateAsyncCity(string name);

        Task<string> CreateAsyncArea(string name, string cityId, string image);

        Task<string> CreateAsyncStreet(string name, string areaId);

        Task<string> CreateAsyncLocation(int apartment, int number, int flour, int entrance, string streetId);

        Task<string> CreateAsyncLocationObject(string name, string adressId);

        Task<string> CreateAsyncWorkingAreaCourier(string userId, string areaId);

        Task<string> CreateAsyncWorkingAreaRestaurant(string userId, string areaId);

        IEnumerable<T> GetAllAddress<T>(int? count = null);

        IEnumerable<T> GetAllCities<T>(int? count = null);

        IEnumerable<T> GetAllAreas<T>(string id, int? count = null);

        IEnumerable<T> GetAllStreets<T>(string id, int? count = null);

        IEnumerable<T> GetAllLocations<T>(string id, int? count = null);

        IEnumerable<T> GetAllLocationObjects<T>(string id, int? count = null);

        IEnumerable<T> GetAllWorkingAreas<T>(string id, int? count = null);

        T GetByNameCity<T>(string name);

        T GetByIdCity<T>(string id);

        T GetByNameArea<T>(string name);

        T GetByNameStreets<T>(string name);

        T GetByNameLocations<T>(int number);
    }
}
