namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models.Addresses;

    public interface IAddressService
    {
        Task<string> CreateAsyncAddress(string cityId, string areaId, string streetId, string locationId);

        IEnumerable<T> GetAllAddress<T>(int? count = null);

        T GetById<T>(string id);

        Address GetAddress(string id);
    }
}
