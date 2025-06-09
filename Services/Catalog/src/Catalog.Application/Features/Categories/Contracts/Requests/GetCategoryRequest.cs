namespace Catalog.Application.Features.Categories.Contracts.Requests;

public record GetCategoryRequest
{
    public Guid Id { get; init; }
};
