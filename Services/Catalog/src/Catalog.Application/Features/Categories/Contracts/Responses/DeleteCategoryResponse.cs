namespace Catalog.Application.Features.Categories.Contracts.Responses;

public record DeleteCategoryResponse
{
    public bool Success { get; init; }
    public string? Message { get; init; }
}