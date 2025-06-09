using FrontEnd.Api.Features.Admin.Contracts.Requests;
using FrontEnd.Api.Features.Admin.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Api.Features.Admin.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this IEndpointRouteBuilder app)
    {
        var adminGroup = app.MapGroup("/api/admin")
            .WithTags("Admin");

        // Creates a new category
        adminGroup.MapPost("/CreateCategory",
            async ([FromBody] CreateCategoryRequest request, [FromServices] CreateCategoryUseCase useCase) =>
            {
                var categoryId = await useCase.ExecuteAsync(request);
                return Results.Created($"/api/shop/GetCategoryDetails/{categoryId}", categoryId);
            })
            .WithSummary("Creates a category")
            .WithDescription("Creates a new product category. The category details are provided in the request body.");

        // Updates an existing category
        adminGroup.MapPut("/UpdateCategory/{id}",
            async (int id, [FromBody] UpdateCategoryRequest request, [FromServices] UpdateCategoryUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id, request);
                return Results.NoContent();
            })
            .WithSummary("Updates a category")
            .WithDescription("Updates an existing product category. The category ID is provided in the URL, and the updated details are in the request body.");

        // Deletes a category by ID
        adminGroup.MapDelete("/DeleteCategory/{id}",
            async (int id, [FromServices] DeleteCategoryUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id);
                return Results.NoContent();
            })
            .WithSummary("Deletes a category")
            .WithDescription("Deletes a product category by ID.");

        adminGroup.MapPost("/CreateProduct",
            async ([FromBody] CreateProductRequest request, [FromServices] CreateProductUseCase useCase) =>
            {
                var productId = await useCase.ExecuteAsync(request);
                return Results.Created($"/api/shop/GetProductDetails/{productId}", productId);
            })
            .WithSummary("Creates a product")
            .WithDescription("Creates a new product. The product details are provided in the request body.");

        // Updates an existing product
        adminGroup.MapPut("/UpdateProduct/{id}",
            async (int id, [FromBody] UpdateProductRequest request, [FromServices] UpdateProductUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id, request);
                return Results.NoContent();
            })
            .WithSummary("Updates a product")
            .WithDescription("Updates an existing product. The product ID is provided in the URL, and the updated details are in the request body.");

        // Deletes a product by ID
        adminGroup.MapDelete("/DeleteProduct/{id}",
            async (int id, [FromServices] DeleteProductUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id);
                return Results.NoContent();
            })
            .WithSummary("Deletes a product")
            .WithDescription("Deletes a product by ID.");
    }
}