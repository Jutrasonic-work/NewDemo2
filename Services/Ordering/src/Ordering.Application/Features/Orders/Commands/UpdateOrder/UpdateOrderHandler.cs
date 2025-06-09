using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Application.Contracts;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IOrderRepository orderRepository, ICatalogService catalogService) : IRequestHandler<UpdateOrderCommand, Unit>
{
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id);
        if (order == null)
            throw new NotFoundException($"Order with ID {request.Id} not found");

        order.ShippingAddress = request.ShippingAddress;
        order.PhoneNumber = request.PhoneNumber;

        if (request.OrderLines.Any())
        {
            order.OrderItems.Clear();
            decimal totalAmount = 0;

            foreach (var line in request.OrderLines)
            {
                var product = await catalogService.GetProductById(line.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product with ID {line.ProductId} not found");

                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = line.ProductId,
                    Quantity = line.Quantity,
                    UnitPrice = product.Price
                };

                order.OrderItems.Add(orderItem);
                totalAmount += orderItem.UnitPrice * line.Quantity;
            }

            order.TotalAmount = totalAmount;
        }

        await orderRepository.UpdateAsync(order);
        return Unit.Value;
    }
} 