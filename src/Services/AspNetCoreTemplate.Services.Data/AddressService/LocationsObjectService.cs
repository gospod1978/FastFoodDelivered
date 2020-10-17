namespace AspNetCoreTemplate.Services.Data.AddressService
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LocationsObjectService : ILocationsObjectService
    {
        public LocationsObjectService()
        {
        }

        public Task<string> CreateAsyncLocationObject(string name, string adressId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAllLocationsObjects<T>(string id, int? count = null)
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(string id)
        {
            throw new NotImplementedException();
        }

        public T GetByName<T>(string name)
        {
            throw new NotImplementedException();
        }
    }
}
