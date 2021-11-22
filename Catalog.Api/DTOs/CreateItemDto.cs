using System.ComponentModel.DataAnnotations;

namespace Catalog.Api.DTOs
{
    // using our own dto and only adding what exactly is needed for creating an item
    public record CreateItemDto
    {
        [Required]
        public string Name { get; init; }
        
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; init; }
    }
}