namespace AspNetCoreTemplate.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Web.ViewModels.Users;

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

        Task<string> CreateAsyncEmail(EmailModel input);

        IEnumerable<T> GetAllEmail<T>(int? count = null);

        IEnumerable<T> GetAllEmailByUserId<T>(string id, int? count = null);

        T GetEmailById<T>(string id);
    }
}
