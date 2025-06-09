namespace Catalog.Application.Features.Categories.Contracts.Responses;

public record CategoryInfo
{
    public string Name { get; init; } = string.Empty;
    public string IconUrl { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
}
