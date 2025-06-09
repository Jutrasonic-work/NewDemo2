namespace Catalog.Application.Features.Categories.Contracts.Requests;

public record DeleteCategoryRequest
{
    public Guid Id { get; init; }
};