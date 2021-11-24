using Geocache_API.Models;
using Geocache_API.Models.DTOs;
using Geocache_API.Models.Services;
using Geocache_API.Controllers;
using Xunit;
using System;

namespace GeocacheTests
{
    public class GeoCacheTests : Mock
    {
        [Fact]
        public async void CanCreateCacheAndItems()
        {
            CacheService cacheService = new(_dbContext);
            Cache newCache = new()
            {
                Name = "testCache",
                lat = 30.05,
                lon = 144.6705,
                itemCount = 0

            };
            CacheDTO createdCache = await cacheService.Create(newCache);
            CacheDTO cacheFromTable = await cacheService.GetCache(createdCache.Id);
            Assert.Equal(30.05, cacheFromTable.lat);

            ItemService itemService = new(_dbContext);
            NewItemDTO newItem = new()
            {
                Name = "testItem",
                Cache = 0,
                Description = "item created to assert create functionality",
                ExpireDay = 1,
                ExpireMonth = 2,
                ExpireYear = 2200
            };
            Item createdItem = await itemService.Create(newItem);
            Item itemFromTable = await itemService.GetItem(createdItem.Id);
            Assert.Equal("item created to assert create functionality", itemFromTable.Description);
        }
        [Fact]
        public async void CanMoveItems()
        {
            CacheService cacheService = new(_dbContext);
            ItemService itemService = new(_dbContext);

            Cache newCache = new()
            {
                Name = "departureCache",
                lat = 1,
                lon = 1,
                itemCount = 0
            };
            Cache nextCache = new()
            {
                Name = "recieverCache",
                lat = 2,
                lon = 2,
                itemCount = 0
            };
            CacheDTO createdCache1 = await cacheService.Create(newCache);
            CacheDTO createdCache2 = await cacheService.Create(nextCache);

            NewItemDTO newItem = new()
            {
                Name = "testItem",
                Cache = createdCache1.Id,
                Description = "item created to assert move functionality",
                ExpireDay = 1,
                ExpireMonth = 3,
                ExpireYear = 2200
            };
            Item createdItem = await itemService.Create(newItem);
            createdCache1 = await cacheService.GetCache(createdCache1.Id);
            Assert.Equal(1, createdCache1.itemCount);
            Item movedItem = await itemService.MoveItem(createdItem.Id, createdCache2.Id);
            createdCache1 = await cacheService.GetCache(createdCache1.Id);
            createdCache2 = await cacheService.GetCache(createdCache2.Id);
            Assert.Equal(0, createdCache1.itemCount);
            Assert.Equal(1, createdCache2.itemCount);
            Assert.Equal(createdCache2.Id, movedItem.Cache);
        }
        [Fact]
        public async void CannotOverFill()
        {
            CacheService cacheService = new(_dbContext);
            ItemService itemService = new(_dbContext);

            Cache newCache = new()
            {
                Name = "fullCache",
                lat = 13,
                lon = 13,
                itemCount = 3
            };

            CacheDTO createdCache = await cacheService.Create(newCache);

            NewItemDTO newItem = new()
            {
                Name = "testItem",
                Cache = createdCache.Id,
                Description = "item created to assert full cache functionality",
                ExpireDay = 4,
                ExpireMonth = 4,
                ExpireYear = 2200
            };
            await Assert.ThrowsAsync<InvalidOperationException>(() => itemService.Create(newItem));
            newItem.Cache = 0;
            Item createdItem = await itemService.Create(newItem);
            await Assert.ThrowsAsync<InvalidOperationException>(() => itemService.MoveItem(createdItem.Id, createdCache.Id));
        }
    }
}
