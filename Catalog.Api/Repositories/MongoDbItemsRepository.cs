using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    class MongoDbItemsRepository : IItemsRepository
    {
        // we want to store a collection, all documents are stored in a collection
        private readonly IMongoCollection<Item> itemsCollection;
        private const string databaseName = "catalog";
        private const string collectionName = "items";
        // filter object
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        // Constructor
        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            // These will be created if they do not exist due to MongoDB driver
            // reference to our database
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            // ref to our collection
            itemsCollection = database.GetCollection<Item>(collectionName);
        }

        // Creating the Item in DB
        public async Task CreateItemAsync(Item item)
        {
            await itemsCollection.InsertOneAsync(item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            // build filter
            var filter = filterBuilder.Eq(item => item.Id, id);
            await itemsCollection.DeleteOneAsync(filter);
        }

        // Get an item by GUID
        public async Task<Item> GetItemAsync(Guid id)
        {
            // build filter
            var filter = filterBuilder.Eq(item => item.Id, id);
            return await itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        // Get all items
        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        // Update an Item
        public async Task UpdateItemAsync(Item item)
        {
            // build a filter
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            // replace the item with our new one
            await itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}