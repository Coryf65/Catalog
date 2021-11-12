using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    class MongoDbItemsRepository : IItemsRepository
    {

        // we want to store a collection, all documents are stored in a collection
        private readonly IMongoCollection<Item> itemsCollection;
        private const string databaseName = "catalog";
        private const string collectionName = "items";


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
            throw new NotImplementedException();
        }

        public Item GetItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems()
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}