using Microsoft.EntityFrameworkCore;
using Geocache_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.Services
{
    public class CacheService
    {
        private GeoCacheDbContext _context { get; }
        public CacheService(GeoCacheDbContext context)
        {
            _context = context;
        }

        public async Task<Cache> Create(Cache cache)
        {

        }

        public async Task<List<Cache>> GetCaches()
        {

        }

        public async Task<Cache> GetCache(int id)
        {

        }

        public async Task<Cache> UpdateCache(int Id, Cache cache)
        {

        }
    }
}
