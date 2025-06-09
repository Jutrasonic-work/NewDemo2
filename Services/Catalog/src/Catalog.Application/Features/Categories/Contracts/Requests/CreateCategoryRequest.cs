namespace Catalog.Application.Features.Categories.Contracts.Requests;

public record CreateCategoryRequest
{
    public required string Name { get; init; }
    public required string IconUrl { get; init; }
    public required string Color { get; init; }
};
