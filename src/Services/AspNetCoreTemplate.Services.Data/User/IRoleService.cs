﻿namespace AspNetCoreTemplate.Services.Data.User
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoleService
    {
        Task<string> CreateAsyncRole(string name);

        IEnumerable<T> GetAll<T>(int? count = null);

        string GetById<T>(string id);

        Task DeleteByName(string name);

        Task DeleteById(string id);
    }
}