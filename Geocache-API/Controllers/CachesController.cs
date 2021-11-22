using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Geocache_API.Data;
using Geocache_API.Models;

namespace Geocache_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachesController : ControllerBase
    {
        private readonly GeoCacheDbContext _context;

        public CachesController(GeoCacheDbContext context)
        {
            _context = context;
        }

        // GET: api/Caches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cache>>> GetCaches()
        {
            return await _context.Caches.ToListAsync();
        }

        // GET: api/Caches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cache>> GetCache(int id)
        {
            var cache = await _context.Caches.FindAsync(id);

            if (cache == null)
            {
                return NotFound();
            }

            return cache;
        }

        // PUT: api/Caches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCache(int id, Cache cache)
        {
            if (id != cache.Id)
            {
                return BadRequest();
            }

            _context.Entry(cache).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CacheExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Caches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cache>> PostCache(Cache cache)
        {
            _context.Caches.Add(cache);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCache", new { id = cache.Id }, cache);
        }

        // DELETE: api/Caches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCache(int id)
        {
            var cache = await _context.Caches.FindAsync(id);
            if (cache == null)
            {
                return NotFound();
            }

            _context.Caches.Remove(cache);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CacheExists(int id)
        {
            return _context.Caches.Any(e => e.Id == id);
        }
    }
}
