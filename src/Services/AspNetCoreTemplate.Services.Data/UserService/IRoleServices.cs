namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoleServices
    {
        Task<string> CreateAsyncRole(string name);

        IEnumerable<T> GetAll<T>(int? count = null);

        string GetById<T>(string id);

        Task DeleteByName(string name);

        Task DeleteById(string id);
    }
}
