using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.Interfaces
{
    public interface ICache
    {
        Task<Cache> Create(Cache cache);

        Task<Cache> GetCache(int Id);

        Task<List<Cache>> GetCaches();

        Task<Cache> UpadateCache(int Id, Cache cache);

        void Delete(int Id);
    }
}
