namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocationsService
    {
        Task<string> CreateAsyncLocation(int apartment, int number, int flour, int entrance, string streetId);

        IEnumerable<T> GetAllLocations<T>(string id, int? count = null);

        T GetByNumber<T>(int number);

        T GetById<T>(string id);

        T GetByFlour<T>(int flour);

        T GetByEntrance<T>(int entrance);

        T GetByApartament<T>(int apartament);
    }
}
