using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Geocache_API.Models;
using Geocache_API.Models.DTOs;
using Geocache_API.Models.Interfaces;
using System.Text.RegularExpressions;

namespace Geocache_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItem _itemService;

        public ItemsController(IItem service)
        {
            _itemService = service;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
            return await _itemService.GetItems();
        }

        //GET: api/items/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Item>>> GetActiveItems()
        {
            return await _itemService.GetActiveItems();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _itemService.GetItem(id);
            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(int id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest("Id does not match request body.");
            }
            var updatedItem = await _itemService.UpdateItem(id, item);

            return Ok(updatedItem);
        }

        [HttpPut("{id}/{cache}")]
        public async Task<ActionResult<Item>> MoveItem(int id, int cache)
        {
            var thisItem = await _itemService.GetItem(id);
            if(DateTime.Compare(thisItem.Expires, DateTime.Now) <= 0)
            {
                return BadRequest("Expired items may not be assigned to a new cache.");
            }

            var updatedItem = await _itemService.MoveItem(id, cache);
            
            return Ok(updatedItem);
        }

        // POST: api/Items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(NewItemDTO item)
        {
            Regex checkName = new Regex("^[A-Za-z0-9 ]{1,50}$");
            if(checkName.IsMatch(item.Name) == false)
            {
                return BadRequest("Item names must be no more than 50 characters and contain only letters, numbers, and spaces.");
            }

            List<Item> items = await _itemService.GetItems();
            if (items.Exists(i => i.Name == item.Name))
            {
                return BadRequest("Item names must be unique.");
            }

            var newItem = await _itemService.Create(item);
            return CreatedAtAction("GetItem", new { id = newItem.Id }, newItem);
        }

        //PUT: api/Items/remove
        [HttpPut("remove/{id}")]
        public async Task<ActionResult<Item>> RemoveItem(int id)
        {
            return await _itemService.RemoveItem(id);
        }

        //PUT: api/Items/clear
        [HttpPut("clear")]
        public async Task<ActionResult<List<Item>>> ClearExpiredItems()
        {
            return await _itemService.ClearExpiredItems();
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            await _itemService.DeleteItem(id);
            return NoContent();
        }
    }
}
