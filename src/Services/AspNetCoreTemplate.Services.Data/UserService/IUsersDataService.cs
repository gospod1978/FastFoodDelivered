namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Threading.Tasks;

    public interface IUsersDataService
    {
        Task<string> CreateAsyncUserData(string name, string userId);

        T GetByUserName<T>(string name);

        T GetByUserId<T>(string id);

        string GetName(string id);

        string GetId(string id);

        string GetByDataId(string id);

        T GetByDataIdG<T>(string id);
    }
}
