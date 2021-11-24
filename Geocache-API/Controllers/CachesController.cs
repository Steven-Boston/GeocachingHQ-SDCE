using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Geocache_API.Data;
using Geocache_API.Models;
using Geocache_API.Models.Interfaces;
using Geocache_API.Models.Services;
using Geocache_API.Models.DTOs;

namespace Geocache_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachesController : ControllerBase
    {
        private readonly ICache _cacheService;

        public CachesController(ICache service)
        {
            _cacheService = service;
        }

        // GET: api/Caches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CacheDTO>>> GetCaches()
        {
            return await _cacheService.GetCaches();
        }

        // GET: api/Caches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CacheDTO>> GetCache(int id)
        {
            var cache = await _cacheService.GetCache(id);

            if (cache == null)
            {
                return NotFound();
            }

            return cache;
        }

        // PUT: api/Caches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCache(int id, Cache cache)
        {
            if (id != cache.Id)
            {
                return BadRequest();
            }

            var updatedCache = await _cacheService.UpdateCache(id, cache);

            return Ok(updatedCache);
        }

        // POST: api/Caches
        [HttpPost]
        public async Task<ActionResult<CacheDTO>> PostCache(NewCacheDTO cache)
        {
            CacheDTO newCache = await _cacheService.Create(new Cache() { 
                Name = cache.Name,
                lat = cache.lat,
                lon = cache.lon,
                itemCount = 0
            });

            return CreatedAtAction("GetCache", new { id = newCache.Id }, newCache);
        }

        // DELETE: api/Caches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCache(int id)
        { 
            await _cacheService.Delete(id);

            return NoContent();
        }
    }
}
