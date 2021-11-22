using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using System;
using System.Linq;
using Catalog.DTOs;
using System.Threading.Tasks;

namespace Catalog.Controllers
{
    // Marks this as an API controller
    [ApiController]
    [Route("[controller]")] // GET /items
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repo;

        public ItemsController(IItemsRepository repository)
        {
            this.repo = repository;
        }

        [HttpGet] // declare the HTTP Route, like GET /items
        // Setting up the contract
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {        
            // We need to wrap the call to complete Then when it's done we can linq it
            var items = (await repo.GetItemsAsync()).Select( item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        // how we will handle the extra data
        // GET /items/{id}, like /items/2
        public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
        {
            var item = await repo.GetItemAsync(id);

            // if we cannot find the given item
            if (item is null)
            {
                return NotFound();
            }
            
            return item.AsDto();
        }

        // Create action
        // POST /items
        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.Now
            };

            await repo.CreateItemAsync(item);

            // convention is to create and return the item created
            // so we are returning how to get that item saved, the id just saved, and the entire item
            
            // return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.AsDto());
            
            return CreatedAtAction(nameof(GetItemAsync), new { id = item.Id}, item.AsDto());
        }

        // PUT usually don't return
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = await repo.GetItemAsync(id); // call it here

            if (existingItem is null)
            {
                return NotFound();
            }

            // found something, we can use with when using Record types
            Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            await repo.UpdateItemAsync(updatedItem); // need to await here as well

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemAsync(Guid id)
        {
            var existingItem = await repo.GetItemAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            await repo.DeleteItemAsync(id);

            return NoContent();
        }

    }
}