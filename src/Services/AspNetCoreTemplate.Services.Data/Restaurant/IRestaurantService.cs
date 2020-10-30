namespace AspNetCoreTemplate.Services.Data.Restaurant
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRestaurantService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        Task<string> CreateAsync(string image, string phone, string workingAreaId, string userId, string areaId);

        T GetByUserId<T>(string id);

        IEnumerable<T> GetAllYes<T>(int? count = null);

        IEnumerable<T> GetAllNo<T>(int? count = null);

        void MadeIsRestaurant(string id);

        void MadeIsNoRestaurant(string id);
    }
}
