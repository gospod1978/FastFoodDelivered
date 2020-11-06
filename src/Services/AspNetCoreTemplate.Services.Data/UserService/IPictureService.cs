namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPictureService
    {
        Task<string> CreateAsyncImage(string name, string exstention, byte[] data, string orderId);

        IEnumerable<T> GetAllImagesByOrderId<T>(string id, int? count = null);

        IEnumerable<T> GetAllImages<T>(int? count = null);

        IEnumerable<T> GetAllImagesByOrderNoActive<T>(string id, int? count = null);

        IEnumerable<T> GetAllImagesNoActive<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        void DeleteById(string id);

        void DeleteByName(string name);

        T GetByNameDelete<T>(string name);

        T GetByIdDelete<T>(string id);

        T GetByOrderId<T>(string orderId);
    }
}
