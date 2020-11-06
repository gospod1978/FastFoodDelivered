namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<string> AddRoleToUser<T>(string userName, string role);

        T GetById<T>(string id);

        string GetUserName(string name);

        void AddRoleCourier(string userName, string role);

        T GetByUserByUserDataId<T>(string id);

        string GetUserByRestaurantId(string id);
    }
}
