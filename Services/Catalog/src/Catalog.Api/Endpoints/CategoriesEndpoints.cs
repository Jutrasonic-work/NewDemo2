using Catalog.Application.Features.Categories.Contracts.Requests;
using Catalog.Application.Features.Categories.Contracts.Responses;
using Catalog.Application.Features.Categories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpointsAsync(this IEndpointRouteBuilder app)
    {
        // Gets a list of categories with optional filtering and pagination
        app.MapGet("/api/categories",
            async ([AsParameters] GetCategoriesRequest request, [FromServices] ICategoryService service) =>
            {
                GetCategoriesResponse categories = await service.GetListAsync(request);
                //return Results.Ok(response.Categories);
                return Results.Ok(categories);
            })
            .WithSummary("Get list of categories")
            .WithDescription("Retrieves a list of categories with optional filtering and pagination.")
            .WithTags("Categories");

        // Gets a single category by ID
        app.MapGet("/api/categories/{id}",
            async (Guid id, [FromServices] ICategoryService service) =>
            {
                var request = new GetCategoryRequest { Id = id };
                GetCategoryResponse category = await service.GetAsync(request);
                if (category is null)
                    return Results.NotFound();
                return Results.Ok(category);
            })
            .WithSummary("Get a category by ID")
            .WithDescription("Retrieves a category by its unique identifier.")
            .WithTags("Categories");

        // Creates a new category
        app.MapPost("/api/categories",
            async ([FromBody] CreateCategoryRequest request, [FromServices] ICategoryService service) =>
            {
                var id = await service.CreateAsync(request);
                return Results.Created($"/api/categories/{id}", id);
            })
            .WithSummary("Create a new category")
            .WithDescription("Creates a new category with the provided details.")
            .WithTags("Categories");
        //.RequireAuthorization("Admin");

        // Updates an existing category
        app.MapPut("/api/categories/{id}",
            async (Guid id, [FromBody] UpdateCategoryRequest request, [FromServices] ICategoryService service) =>
            {
                if (id != request.Id) return Results.BadRequest();
                await service.UpdateAsync(request);
                return Results.NoContent();
            })
            .WithSummary("Update an existing category")
            .WithDescription("Updates an existing category with the provided details.")
            .WithTags("Categories");
        //.RequireAuthorization("Admin");

        // Deletes a category by ID
        app.MapDelete("/api/categories/{id}",
            async (Guid id, [FromServices] ICategoryService service) =>
            {
                await service.DeleteAsync(new DeleteCategoryRequest { Id = id });
                return Results.NoContent();
            })
            .WithSummary("Delete a category")
            .WithDescription("Deletes a category by its unique identifier.")
            .WithTags("Categories")
            .Accepts<DeleteCategoryRequest>("application/json");
            //.RequireAuthorization("Admin");

    }
}