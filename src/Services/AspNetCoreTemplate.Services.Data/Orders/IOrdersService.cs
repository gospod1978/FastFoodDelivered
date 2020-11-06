namespace AspNetCoreTemplate.Services.Data.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrdersService
    {
        Task<string> CreateAsyncMenu(string name, string description, string restaurantId, string timePreparation, decimal price, int categoryId);

        IEnumerable<T> GetAllOrders<T>(int? count = null);

        IEnumerable<T> GetAllByUserId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByRestaurantName<T>(string name, int? count = null);

        IEnumerable<T> GetAllByRestaurantId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByCourierId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByCategoryName<T>(string name, int? count = null);

        IEnumerable<T> GetAllByCategoryId<T>(int id, int? count = null);

        IEnumerable<T> GetAllByPromotionName<T>(string name, int? count = null);

        IEnumerable<T> GetAllByPromotionId<T>(string id, int? count = null);

        IEnumerable<T> GetAllByPrice<T>(decimal price, int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        void AddImageToOrder(string orderId, string pictureId);

        string ChangePromotion(string orderId, string promotionId);
    }
}
