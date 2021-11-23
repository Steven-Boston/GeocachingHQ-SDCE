using Microsoft.EntityFrameworkCore;
using Geocache_API.Data;
using Geocache_API.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.Services
{
    public class ItemService : IItem
    {
        private GeoCacheDbContext _context;
        public ItemService(GeoCacheDbContext context)
        {
            _context = context;
        }

        public async Task<Item> Create(Item item)
        {
            _context.Entry(item).State = EntityState.Added;

            await _context.SaveChangesAsync();

            return await GetItem(item.Id);
        }

        public Task<List<Item>> GetItems()
        {
            return _context.Items.ToListAsync();
        }

        public async Task<List<Item>> GetActiveItems()
        {
            return await _context.Items
                .Where(item => item.Expires < DateTime.Now)
                .ToListAsync();
        }

        public async Task<List<Item>> GetCacheItems(int CacheId)
        {
            return await _context.Items
                .Where(item => item.Cache == CacheId)
                .ToListAsync();
        }

        public async Task<Item> GetItem(int Id)
        {
            return await _context.Items.FindAsync(Id);
        }

        public async Task<Item> UpdateItem(int Id, Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await _context.Items.FindAsync(Id);
        }

        public async Task<Item> MoveItem(int ItemId, int CacheId)
        {
            Item item = await _context.Items.FindAsync(ItemId);
            item.Cache = CacheId;
            return await UpdateItem(ItemId, item);
        }

        public async Task DeleteItem(int Id)
        {
            Item item = await _context.Items.FindAsync(Id);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
