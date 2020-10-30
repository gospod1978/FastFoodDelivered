namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDocumentsService
    {
        Task<string> CreateAsyncDocument(string name, string fileType, byte[] data, string userDataId);

        IEnumerable<T> GetAllDocumentsByUser<T>(string id, int? count = null);

        IEnumerable<T> GetAllDocuments<T>(int? count = null);

        IEnumerable<T> GetAllDocumentsByUserNoActive<T>(string id, int? count = null);

        IEnumerable<T> GetAllDocumentsNoActive<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        void DeleteById(string id);

        void DeleteByName(string name);

        T GetByNameDelete<T>(string name);

        T GetByIdDelete<T>(string id);
    }
}
