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
    public class ItemService : IItem
    {
        private GeoCacheDbContext _context;
        public ItemService(GeoCacheDbContext context)
        {
            _context = context;
        }

        public async Task<Item> Create(NewItemDTO itemData)
        {
            DateTime expires = new DateTime(itemData.ExpireYear, itemData.ExpireMonth, itemData.ExpireDay);
            
            //check if intended cache is valid and update itemCount
            if(itemData.Cache > 0)
            {
                Cache target = await _context.Caches.FindAsync(itemData.Cache);
                if(target.itemCount >= 3)
                {
                    throw new InvalidOperationException("Target cache is full.");
                }
                else
                {
                    target.itemCount++;
                    _context.Entry(target).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
            }
            
            Item item = new Item()
            {
                Name = itemData.Name,
                Description = itemData.Description,
                Cache = itemData.Cache,
                Activated = DateTime.Now,
                Expires = expires
            };
            
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
                .Where(item => DateTime.Compare(item.Expires, DateTime.Now) > 0)
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
            Cache target = await _context.Caches.FindAsync(CacheId);
            if (target.itemCount >= 3)
            {
                throw new InvalidOperationException("Target cache is full.");
            }
            else
            {
                Cache departure = await _context.Caches.FindAsync(CacheId);
                target.itemCount++;
                departure.itemCount--;
                _context.Entry(departure).State = EntityState.Modified;
                _context.Entry(target).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }


            Item item = await _context.Items.FindAsync(ItemId);
            item.Cache = CacheId;
            return await UpdateItem(ItemId, item);
        }

        public async Task<Item> RemoveItem(int itemId)
        {
            Item thisItem = await _context.Items.FindAsync(itemId);
            Cache target = await _context.Caches.FindAsync(thisItem.Cache);
            thisItem.Cache = 0;
            target.itemCount--;
            _context.Entry(thisItem).State = EntityState.Modified;
            _context.Entry(target).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await GetItem(itemId);

        }

        public async Task<List<Item>> ClearExpiredItems()
        {
            List<Item> expirees = await _context.Items
                .Where(item => DateTime.Compare(item.Expires, DateTime.Now) <=0)
                .ToListAsync();

            expirees.ForEach(async exp =>
            {
                await RemoveItem(exp.Id);
            });

            return await _context.Items
                .Where(item => DateTime.Compare(item.Expires, DateTime.Now) <= 0)
                .ToListAsync();
        }

        public async Task DeleteItem(int Id)
        {
            Item item = await _context.Items.FindAsync(Id);
            _context.Entry(item).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
