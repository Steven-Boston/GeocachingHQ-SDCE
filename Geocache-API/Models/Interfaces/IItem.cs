using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geocache_API.Models.DTOs;

namespace Geocache_API.Models.Interfaces
{
    public interface IItem
    {
        Task<Item> Create(NewItemDTO item);

        Task<List<Item>> GetItems();

        Task<List<Item>> GetActiveItems();

        Task<List<Item>> GetCacheItems(int CacheId);

        Task<Item> GetItem(int Id);

        Task<Item> UpdateItem(int Id, Item item);

        Task<Item> MoveItem(int ItemId, int CacheId);

        Task DeleteItem(int Id);
    }
}
