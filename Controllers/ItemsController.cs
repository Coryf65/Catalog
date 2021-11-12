using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using System;
using System.Linq;
using Catalog.DTOs;

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
        public IEnumerable<ItemDto> GetItems()
        {        
            var items = repo.GetItems().Select( item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        // how we will handle the extra data
        // GET /items/{id}, like /items/2
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repo.GetItem(id);

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
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.Now
            };

            repo.CreateItem(item);

            // convention is to create and return the item created
            // so we are returning how to get that item saved, the id just saved, and the entire item
            return CreatedAtAction(nameof(GetItem), new { id = item.Id}, item.AsDto());
        }

        // PUT usually don't return
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            var existingItem = repo.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            // found something, we can use with when using Record types
            Item updatedItem = existingItem with {
                Name = itemDto.Name,
                Price = itemDto.Price
            };

            repo.UpdateItem(updatedItem);

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existingItem = repo.GetItem(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            repo.DeleteItem(id);

            return NoContent();
        }

    }
}