using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geocache_API.Models.DTOs;

namespace Geocache_API.Models.Interfaces
{
    public interface ICache
    {
        Task<CacheDTO> Create(Cache cache);

        Task<CacheDTO> GetCache(int Id);

        Task<List<CacheDTO>> GetCaches();

        Task<CacheDTO> UpdateCache(int Id, Cache cache);

        Task Delete(int Id);
    }
}
