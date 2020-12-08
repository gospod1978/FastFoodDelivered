namespace AspNetCoreTemplate.Services.Data.Addresses
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AspNetCoreTemplate.Data.Models.Addresses;
    using AspNetCoreTemplate.Web.ViewModels.Addresses;

    public interface IAddressService
    {
        Task<string> CreateAsyncAddress(string cityId, string areaId, string streetId, string locationId);

        IEnumerable<T> GetAllAddress<T>(int? count = null);

        T GetById<T>(string id);

        IndexAddressViewModel GetAddress(string id);

        Task<string> CreateAsyncWorkingArea(string areaId, string userId);

        WorkingArea GetByWorkingAreaByUserId(string id);

        void ChangeWorkingAreaOn(string id, string areaId);

        void ChangeWorkingAreaOff(string id);
    }
}
