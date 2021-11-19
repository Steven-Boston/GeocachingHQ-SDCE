using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geocache_API.Models.Interfaces
{
    interface IItem
    {
        Task<Item> Create(Item item);

        Task<List<Item>> GetItems();

        Task<List<Item>> GetActiveItems();

        Task<List<Item>> GetCacheItems(int CacheId);

        Task<Item> GetItem(int Id);

        Task<Item> UpdateItem(int Id, Item item);

        Task<Item> MoveItem(int ItemId, int CacheId);

        void DeleteItem(int Id);
    }
}
