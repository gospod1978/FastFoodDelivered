namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAddressService
    {
        Task<string> CreateAsyncAddress(string cityId, string areaId, string streetId, string locationId);

        IEnumerable<T> GetAllAddress<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);
    }
}
