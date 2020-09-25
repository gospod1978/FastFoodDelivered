namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserServices
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<string> AddRoleToUser<T>(string userName, string role);

        T GetById<T>(string id);
    }
}
