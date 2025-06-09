using Catalog.Domain.Enums;

namespace Catalog.Infrastructure.Dao.Products;

public class GetProductsDao
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryColor { get; set; } = string.Empty;
    public string CategoryIconUrl { get; set; } = string.Empty;
    public ProductUnit Unit { get; set; } = ProductUnit.Piece;
    public bool InStock { get; set; } = false;
}

