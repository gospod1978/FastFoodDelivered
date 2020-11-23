namespace AspNetCoreTemplate.Services.Data.Orders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPurchaseService
    {
        Task<string> CreateAsyncMenu(string orderId, string userId, string courierId, string restaurantID, string promotionType, decimal price, decimal deliveryPrice);

        IEnumerable<T> GetAll<T>(string id, string role, int? count = null);

        IEnumerable<T> GetAllByUserId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByCourierId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByRestaurantId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByStatus<T>(string status, int? count = null);

        IEnumerable<T> GetAllByPromotionType<T>(string promotionType, int? count = null);

        Task<string> ChangeStatus(string purchazeId, string status);

        Dictionary<string, decimal> FindCourier(string restaurantLocation);

        decimal PriceCourier(string userAreaId, string restAreaId);

        string CourierIdFind(string restAreaId);

        T GetById<T>(string id);
    }
}
