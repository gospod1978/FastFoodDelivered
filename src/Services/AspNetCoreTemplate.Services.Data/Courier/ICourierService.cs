namespace AspNetCoreTemplate.Services.Data.Courier
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICourierService
    {
        IEnumerable<T> GetAll<T>(int? count = null);

        T GetByName<T>(string name);

        T GetById<T>(string id);

        Task<string> CreateAsync(string image, string phone, string vechileID, DateTime birthday, string workingAreaId, string userId, string areaId);

        T GetByUserId<T>(string id);

        IEnumerable<T> GetAllYes<T>(int? count = null);

        IEnumerable<T> GetAllNo<T>(int? count = null);

        void MadeIsCourier(string id);

        void MadeIsNoCourier(string id);

        Task<string> CreateWorkingAreaByCourierId(string courierId, string areaId);
    }
}
