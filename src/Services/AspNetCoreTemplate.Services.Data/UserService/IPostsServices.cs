namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPostsServices
    {
        Task<int> CreateAsync(string title, string content, int categoryId, string userId);

        T GetById<T>(int id);

        IEnumerable<T> GetByCategoryId<T>(int categoryId, int? take = null, int skip = 0);

        int GetCountByCategoryId(int categoryId);
    }
}
