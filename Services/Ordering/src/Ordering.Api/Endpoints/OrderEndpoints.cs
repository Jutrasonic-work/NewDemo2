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
        .WithName("GetOrders")
        .WithOpenApi();

        // Gets order by ID
        group.MapGet("/{id}",
            async (Guid id, IMediator mediator) =>
            {
                var query = new GetOrderByIdQuery(id);
                var order = await mediator.Send(query);
                return order is null ? Results.NotFound() : Results.Ok(order);
            })
        .WithName("GetOrderById")
        .WithOpenApi();

        // Creates order
        group.MapPost("/",
            async (CreateOrderCommand command, IMediator mediator) =>
            {
                var orderId = await mediator.Send(command);
                return Results.Created($"/api/orders/{orderId}", orderId);
            })
        .WithName("CreateOrder")
        .WithOpenApi();

        // Updates order
        group.MapPut("/{id}",
            async (Guid id, UpdateOrderCommand command, IMediator mediator) =>
            {
                if (id != command.Id)
                    return Results.BadRequest("ID mismatch");

                var result = await mediator.Send(command);
                return result.Equals(Unit.Value) ? Results.NoContent() : Results.NotFound();
            })
        .WithName("UpdateOrder")
        .WithOpenApi();

        // Deletes order
        group.MapDelete("/{id}",
            async (Guid id, IMediator mediator) =>
            {
                var command = new DeleteOrderCommand { Id = id };
                var result = await mediator.Send(command);
                return result.Equals(Unit.Value) ? Results.NoContent() : Results.NotFound();
            })
        .WithName("DeleteOrder")
        .WithOpenApi();
    }
}