using Catalog.Application.Features.Products.Contracts.Requests;
using Catalog.Application.Features.Products.Contracts.Responses;
using Catalog.Application.Features.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductsEndpoints(this IEndpointRouteBuilder app)
    {
        // Gets a list of products with optional filtering
        app.MapGet("/api/products", async ([AsParameters] GetProductsRequest request, [FromServices] IProductService service) =>
            Results.Ok(await service.GetListAsync(request)))
            .WithSummary("Gets a list of products")
            .WithDescription("Retrieves a list of products with optional filtering.")
            .WithTags("Products");

        // Gets a single product by ID
        app.MapGet("/api/products/{id}",
            async (Guid id, [FromServices] IProductService service) =>
            {
                GetProductResponse category = await service.GetAsync(id);
                if (category is null)
                    return Results.NotFound();
                return Results.Ok(category);
            })
            .WithSummary("Gets a product by ID")
            .WithDescription("Retrieves a product by its unique identifier.")
            .WithTags("Products");

        // Creates a new product
        app.MapPost("/api/products",
            async ([FromBody] CreateProductRequest request, [FromServices] IProductService service) =>
            {
                Guid id = await service.CreateAsync(request);
                return Results.Created($"/api/products/{id}", id);
            })
            .WithSummary("Creates a new product")
            .WithDescription("Creates a new product with the provided details.")
            .WithTags("Products");

        // Updates an existing product
        app.MapPut("/api/products/{id}",
            async (Guid id, [FromBody] UpdateProductRequest request, [FromServices] IProductService service) =>
            {
                await service.UpdateAsync(request);
                return Results.NoContent();
            })
            .WithSummary("Updates an existing product")
            .WithDescription("Updates an existing product with the provided details.")
            .WithTags("Products");

        // Deletes a product by ID
        app.MapDelete("/api/products/{id}",
            async (Guid id, [FromServices] IProductService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            })
            .WithSummary("Deletes a product")
            .WithDescription("Deletes a product by its unique identifier.")
            .WithTags("Products");

    }
}