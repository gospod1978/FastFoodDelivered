namespace AspNetCoreTemplate.Services.Data.Courier
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourierService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        Task<string> CreateAsync(string image, string phone, string vechileID, string birthday, string workingAreaId, string userId);
    }
}
