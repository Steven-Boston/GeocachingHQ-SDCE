using Microsoft.EntityFrameworkCore;
using Geocache_API.Data;
using Geocache_API.Models.Interfaces;
using Geocache_API.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Geocache_API.Models.Services
{
    public class CacheService : ICache
    {
        private GeoCacheDbContext _context;
        public CacheService(GeoCacheDbContext context)
        {
            _context = context;
        }

        public async Task<CacheDTO> Create(Cache cache)
        {
            _context.Entry(cache).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return await GetCache(cache.Id);
        }

        public async Task<List<CacheDTO>> GetCaches()
        {
            return await _context.Caches
                .Select(cache => new CacheDTO()
                {
                    Id = cache.Id,
                    Name = cache.Name,
                    lat = cache.lat,
                    lon = cache.lon,
                    itemCount = cache.itemCount,
                    Items = _context.Items.Where(item => item.Cache == cache.Id)
                        .Select(item => new CacheItem()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            Expires = item.Expires
                        })
                        .ToList()
                }).ToListAsync();
        }

        public async Task<CacheDTO> GetCache(int Id)
        {
            return await _context.Caches
                .Select(cache => new CacheDTO()
                {
                    Id = cache.Id,
                    Name = cache.Name,
                    lat = cache.lat,
                    lon = cache.lon,
                    itemCount = cache.itemCount,
                    Items = _context.Items.Where(item => item.Cache == cache.Id)
                        .Select(item => new CacheItem()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Description = item.Description,
                            Expires = item.Expires
                        })
                        .ToList()
                }).FirstOrDefaultAsync(c => c.Id == Id);
        }

        public async Task<CacheDTO> UpdateCache(int Id, Cache cache)
        {
            _context.Entry(cache).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await GetCache(Id);
        }

        public async Task Delete(int Id)
        {
            Cache cache = await _context.Caches.FindAsync(Id);
            _context.Entry(cache).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
