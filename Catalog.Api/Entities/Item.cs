using System;

namespace Catalog.Entities
{
    public record Item
    {
        public Guid Id { get; init; } // only allow setting during initialization
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }        
    }
}