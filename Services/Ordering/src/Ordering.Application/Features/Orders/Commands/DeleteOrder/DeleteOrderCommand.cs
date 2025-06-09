using MediatR;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
} 