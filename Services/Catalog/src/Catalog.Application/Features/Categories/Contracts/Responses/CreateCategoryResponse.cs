namespace Catalog.Application.Features.Categories.Contracts.Responses;

public record CreateCategoryResponse
{
    public Guid Id { get; init; }
}