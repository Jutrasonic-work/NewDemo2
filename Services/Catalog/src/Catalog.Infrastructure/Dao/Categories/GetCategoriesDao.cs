namespace Catalog.Infrastructure.Dao.Categories;

public class GetCategoryDao
{
    public Guid Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;

}