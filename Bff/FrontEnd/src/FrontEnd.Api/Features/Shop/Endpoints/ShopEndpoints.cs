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

        // Gets the current user's cart
        shopGroup.MapGet("/GetCart/{id}",
            async (int id, [FromServices] GetCartUseCase useCase) =>
            {
                var cart = await useCase.ExecuteAsync(id);
                return Results.Ok(cart);
            })
            .WithSummary("Gets Cart")
            .WithDescription("Retrieves the current user's shopping cart by user ID.");

        // Adds a product to the user's cart
        shopGroup.MapPost("/AddToCart",
            async (int id, [FromBody] AddToCartRequest request, [FromServices] AddToCartUseCase useCase) =>
            {
                await useCase.ExecuteAsync(request);
                return Results.NoContent();
            })
            .WithSummary("Adds to Cart")
            .WithDescription("Adds a product to the user's shopping cart. The product ID is provided in the request body.");

        // Updates an item in the user's cart
        shopGroup.MapPut("/UpdateCartItem/{id}",
            async (int id, [FromBody] UpdateCartItemRequest request, [FromServices] UpdateCartItemUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id, request);
                return Results.NoContent();
            })
            .WithSummary("Updates Cart Item")
            .WithDescription("Updates an item in the user's shopping cart. The item ID is provided in the URL, and the updated details are in the request body.");

        // Removes an item from the user's cart
        shopGroup.MapDelete("/RemoveFromCart/{id}",
            async (int id, [FromServices] RemoveFromCartUseCase useCase) =>
            {
                await useCase.ExecuteAsync(id);
                return Results.NoContent();
            })
            .WithSummary("Removes From Cart")
            .WithDescription("Removes an item from the user's shopping cart by item ID.");

        // Clear the user's cart
        //shopGroup.MapDelete("/ClearCart", async (
        //    [FromServices] ClearCartUseCase useCase) =>
        //{
        //    await useCase.ExecuteAsync();
        //    return Results.NoContent();
        //});


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