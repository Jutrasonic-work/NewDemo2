namespace Catalog.Application.Features.Categories.Contracts.Responses;

public record GetCategoriesResponse
{
    public IEnumerable<GetCategoriesItemResponse> Items { get; init; } = Enumerable.Empty<GetCategoriesItemResponse>();

}
