namespace AspNetCoreTemplate.Services.Data.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IImagesService
    {
        Task<string> CreateAsyncImage(string name, string fileType, byte[] data, string userDataId);

        IEnumerable<T> GetAllImagesByUser<T>(string id, int? count = null);

        IEnumerable<T> GetAllImages<T>(int? count = null);

        IEnumerable<T> GetAllImagesByUserNoActive<T>(string id, int? count = null);

        IEnumerable<T> GetAllImagesNoActive<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        void DeleteById(string id);

        void DeleteByName(string name);

        T GetByNameDelete<T>(string name);

        T GetByIdDelete<T>(string id);
    }
}
