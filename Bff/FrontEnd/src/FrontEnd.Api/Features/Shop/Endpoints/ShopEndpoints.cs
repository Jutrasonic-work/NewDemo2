using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Features.Shop.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Api.Features.Shop.Endpoints;

public static class ShopEndpoints
{
    public static void MapShopEndpoints(this IEndpointRouteBuilder app)
    {
        var shopGroup = app.MapGroup("/api/shop")
            .WithTags("Shop");

        // Gets the current user's order
        shopGroup.MapGet("/GetOrder/{id}",
            async (int id, [FromServices] GetOrderUseCase useCase) =>
            {
                var order = await useCase.ExecuteAsync(id);
                return Results.Ok(order);
            })
            .WithSummary("Gets Order")
            .WithDescription("Retrieves the current user's shopping order by user ID.");

        // Adds a product to the user's order
        shopGroup.MapPost("/AddToOrder/{id}",
            async (int id, [FromBody] AddToOrderRequest request, [FromServices] AddToOrderUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id, request);
                return Results.NoContent();
            })
            .WithSummary("Adds to Order")
            .WithDescription("Adds a product to the user's shopping order. The product ID is provided in the request body.");


        // Removes an item from the user's order
        shopGroup.MapDelete("/RemoveFromOrder/{id}",
            async (int id, [FromServices] RemoveFromOrderUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id);
                return Results.NoContent();
            })
            .WithSummary("Removes From Order")
            .WithDescription("Removes an item from the user's shopping order by item ID.");



        // Gets all categories
        shopGroup.MapGet("/GetCategories",
            async ([FromServices] GetCategoriesUseCase useCase) =>
            {
                var categories = await useCase.ExecuteAsync();
                return Results.Ok(categories);
            })
            .WithSummary("Gets Categories")
            .WithDescription("Retrieves all product categories available in the shop.");

        // Gets details of a specific category by ID
        shopGroup.MapGet("/GetCategoryDetails/{id}",
            async (int id, [FromServices] GetCategoryDetailsUseCase useCase) =>
            {
                var category = await useCase.ExecuteAsync(id);
                return category is null
                    ? Results.NotFound()
                    : Results.Ok(category);
            })
            .WithSummary("Gets Category Details")
            .WithDescription("Retrieves details of a specific product category by its ID.");

        // Gets all products
        shopGroup.MapGet("/GetProducts",
            async ([FromServices] GetProductsUseCase useCase) =>
            {
                var products = await useCase.ExecuteAsync();
                return Results.Ok(products);
            })
            .WithSummary("Gets Products")
            .WithDescription("Retrieves all products available in the shop.");


        // Gets details of a specific product by ID
        shopGroup.MapGet("/GetProductDetails/{id}",
            async (int id, [FromServices] GetProductDetailsUseCase useCase) =>
            {
                var product = await useCase.ExecuteAsync(id);
                return product is null
                    ? Results.NotFound()
                    : Results.Ok(product);
            })
            .WithSummary("Gets Product Details")
            .WithDescription("Retrieves details of a specific product by its ID.");
    }
}