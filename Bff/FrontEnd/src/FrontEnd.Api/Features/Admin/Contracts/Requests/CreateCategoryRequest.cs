namespace FrontEnd.Api.Features.Admin.Contracts.Requests;

public record CreateCategoryRequest(
    string Id,
    string Name,
    string Icon,
    string Gradient
); 