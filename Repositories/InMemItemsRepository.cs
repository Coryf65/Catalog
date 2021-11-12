using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
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
        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        // Get item based on GUID
        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        // Create Item
        public void CreateItem(Item item)
        {
            // in mem list, update the list
            items.Add(item);
        }

        public void UpdateItem(Item item)
        {
            // in mem list, so update the list
            var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
            items[index] = item;
        }
    }
}