using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.DeleteOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrders;
using Ordering.Application.Features.Orders.Queries.GetOrderById;

namespace Ordering.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orders")
            .WithTags("Orders");

        // Gets orders
        group.MapGet("/",
            async ([FromQuery] GetOrdersParameters? parameters, IMediator mediator) =>
            {
                var query = new GetOrdersQuery { Parameters = parameters ?? new() };
                var orders = await mediator.Send(query);
                return Results.Ok(orders);
            })
            .WithSummary("Gets a list of orders")
            .WithDescription("Retrieves a list of orders based on optional query parameters such as UserId, FromDate, and ToDate.")

        // Gets order by ID
        group.MapGet("/{id}",
            async (Guid id, IMediator mediator) =>
            {
                var query = new GetOrderByIdQuery(id);
                var order = await mediator.Send(query);
                if (order is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(order);
            })
            .WithSummary("Gets an order by ID")
            .WithDescription("Retrieves a specific order by its ID. If the order does not exist, returns a 404 Not Found response.");

        // Creates order
        group.MapPost("/",
            async (CreateOrderCommand command, IMediator mediator) =>
            {
                var orderId = await mediator.Send(command);
                return Results.Created($"/api/orders/{orderId}", orderId);
            })
            .WithSummary("Creates a new order")
            .WithDescription("Creates a new order with the provided details. Returns the ID of the newly created order.");

        // Updates order
        group.MapPut("/{id}",
            async (Guid id, UpdateOrderCommand command, IMediator mediator) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("ID mismatch");

                var result = await mediator.Send(command);
                if (!result.Equals(Unit.Value))
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            })
            .WithSummary("Updates an existing order")
            .WithDescription("Updates an existing order with the provided details. If the order does not exist, returns a 404 Not Found response.");

        // Deletes order
        group.MapDelete("/{id}",
            async (Guid id, IMediator mediator) =>
            {
                var command = new DeleteOrderCommand { Id = id };
                var result = await mediator.Send(command);
                if (!result.Equals(Unit.Value))
                {
                    return Results.NotFound();
                }
                return Results.NoContent();
            })
            .WithSummary("Deletes an order")
            .WithDescription("Deletes an order by its ID. If the order does not exist, returns a 404 Not Found response.");
    }
}