namespace AspNetCoreTemplate.Services.Data.UserService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoriesService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        Task<int> CreateAsync(string name, string title, string description, string imageUrl);

        T GetById<T>(int id);
    }
}
