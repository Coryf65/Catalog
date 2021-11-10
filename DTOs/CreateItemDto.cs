namespace Catalog.DTOs
{
    // using our own dto and only adding what exactly is needed for creating an item
    public record CreateItemDto
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}