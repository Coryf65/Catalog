using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Entities;

namespace Catalog.Api.Repositories
{    
    /// In memory Data
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = System.DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = System.DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 12, CreatedDate = System.DateTimeOffset.UtcNow }
        };

        // Get all items
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(items); // create a task that is completed and return results
        }

        // Get item based on GUID
        public async Task<Item> GetItemAsync(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        // Create Item
        public async Task CreateItemAsync(Item item)
        {
            // in mem list, update the list
            items.Add(item);
            // we have to return the task that it is complete, as there is no return
            await Task.CompletedTask;
        }

        // Update an item
        public async Task UpdateItemAsync(Item item)
        {
            // in mem list, so update the list
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
            await Task.CompletedTask;
        }

        // Delete an item
        public async Task DeleteItemAsync(Guid id)
        {
            var index = items.FindIndex(existingItem => existingItem.Id == id);
            items.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}