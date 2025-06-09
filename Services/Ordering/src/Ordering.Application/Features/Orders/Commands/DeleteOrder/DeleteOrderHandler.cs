using MediatR;
using Ordering.Domain.Repositories;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand, Unit>
{
    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id);
        if (order == null)
            throw new NotFoundException($"Order with ID {request.Id} not found");

        await orderRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
} 