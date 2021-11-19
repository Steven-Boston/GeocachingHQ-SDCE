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
            _context.Entry(cache).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return await GetCache(cache.Id);
        }

        public async Task<List<Cache>> GetCaches()
        {
            return await _context.Caches.ToListAsync();
        }

        public async Task<Cache> GetCache(int Id)
        {
            return await _context.Caches.FindAsync(Id);
        }

        public async Task<Cache> UpdateCache(int Id, Cache cache)
        {
            _context.Entry(cache).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetCache(Id);
        }

        public async void Delete(int Id)
        {
            Cache cache = await _context.Caches.FindAsync(Id);
            _context.Entry(cache).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
