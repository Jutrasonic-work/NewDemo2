namespace Catalog.Application.Features.Categories.Contracts.Responses;

public class GetCategoriesItemResponse
{
    public Guid Id { get; init; } = Guid.Empty;
    public string Name { get; init; } = string.Empty;
    public string IconUrl { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
}