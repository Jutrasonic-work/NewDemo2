namespace FrontEnd.Api.Features.Admin.Contracts.Requests;

public record UpdateCategoryRequest(
    string Name,
    string Icon,
    string Gradient
); 