namespace Catalog.Application.Features.Categories.Contracts.Responses;

public record UpdateCategoryResponse
{
    public bool Success { get; init; }
    public string? Message { get; init; }
}
