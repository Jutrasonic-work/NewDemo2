using MediatR;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand : IRequest<Guid>
{
    public Guid UserId { get; init; }
    public string ShippingAddress { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public List<OrderItemDto> OrderItems { get; init; } = new();
}

public record OrderItemDto
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
} 