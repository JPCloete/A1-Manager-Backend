using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A1_Manager.Interfaces.Services_Interfaces
{
    public interface IRedisService
    {
        Task<string> GetCacheValueAsync(string key);

        Task SetObjectCacheValueAsync(string key, object value);

        Task SetStringCacheValueAsync(string key, string value);

        Task DeleteCacheValueAsync(string key);
    }
}
