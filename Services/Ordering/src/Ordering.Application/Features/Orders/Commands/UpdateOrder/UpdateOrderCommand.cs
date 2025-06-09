using MediatR;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public string ShippingAddress { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public List<OrderLineDto> OrderLines { get; init; } = new();
}

public record OrderLineDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
} 