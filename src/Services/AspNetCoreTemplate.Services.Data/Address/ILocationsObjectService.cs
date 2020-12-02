namespace AspNetCoreTemplate.Services.Data.Address
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocationsObjectService
    {
        Task<string> CreateAsyncLocationObject(string name, string adressId, string userId);

        IEnumerable<T> GetAllLocationsObjects<T>(int? count = null);

        IEnumerable<T> GetAllByUserId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByAddressId<T>(string id, int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        T GetLocationByUserId<T>(string id, string locationObjName);

        T GetLocationByUserIdOnly<T>(string id);
    }
}
