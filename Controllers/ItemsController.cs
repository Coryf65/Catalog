using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Catalog.Repositories;
using Catalog.Entities;
using System;

namespace Catalog.Controllers
{
    // Marks this as an API controller
    [ApiController]
    [Route("[controller]")] // GET /items
    public class ItemsController : ControllerBase
    {
        private readonly InMemItemsRepository repo;

        public ItemsController()
        {
            repo = new InMemItemsRepository();
        }

        [HttpGet] // declare the HTTP Route, like GET /items
        public IEnumerable<Item> GetItems()
        {        
            var items = repo.GetItems();
            return items;
        }

        [HttpGet("{id}")]
        // how we will handle the extra data
        // GET /items/{id}, like /items/2
        public ActionResult<Item> GetItem(Guid id)
        {
            var item = repo.GetItem(id);

            // if we cannot find the given item
            if (item is null)
            {
                return NotFound();
            }

            // ActionResult allows us to return many different types
            return item;
        }

    }
}