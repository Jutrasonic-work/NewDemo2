using Catalog.Domain.Enums;

namespace Catalog.Infrastructure.Dao.Products;

public class GetProductDao
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int Stock { get; init; } = 0;
    public Guid CategoryId { get; set; }
    public ProductUnit Unit { get; set; } = ProductUnit.Piece; 
}
