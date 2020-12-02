namespace AspNetCoreTemplate.Services.Data.Address
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAreasService
    {
        Task<string> CreateAsyncArea(string name, string cityId, string image);

        Task<string> CreateAsyncWorkingAreaCourier(string userId, string areaId);

        Task<string> CreateAsyncWorkingAreaRestaurant(string userId, string areaId);

        IEnumerable<T> GetAllAreas<T>(string id, int? count = null);

        IEnumerable<T> GetAllWorkingAreas<T>(string id, int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        T GetCityByAreaId<T>(string id);
    }
}
