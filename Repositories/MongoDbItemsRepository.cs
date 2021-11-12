using System;
using System.Collections.Generic;
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
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            // build filter
            var filter = filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);
        }

        // Get an item by GUID
        public Item GetItem(Guid id)
        {
            // build filter
            var filter = filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        // Update an Item
        public void UpdateItem(Item item)
        {
            // build a filter
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            // replace the item with our new one
            itemsCollection.ReplaceOne(filter, item);
        }
    }
}