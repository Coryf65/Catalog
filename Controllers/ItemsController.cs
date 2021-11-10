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

    }
}