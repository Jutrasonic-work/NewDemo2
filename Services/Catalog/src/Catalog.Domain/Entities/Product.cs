using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = new Category();
    public int Stock { get; set; }
    public ProductUnit Unit { get; set; }
    public bool InStock { get; set; }

}
