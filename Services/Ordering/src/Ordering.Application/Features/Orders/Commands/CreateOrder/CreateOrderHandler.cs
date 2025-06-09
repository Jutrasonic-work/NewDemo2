using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;
using Ordering.Application.Contracts;
using Ordering.Application.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IOrderRepository orderRepository, ICatalogService catalogService) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            OrderDate = DateTime.UtcNow,
            ShippingAddress = request.ShippingAddress,
            PhoneNumber = request.PhoneNumber
        };

        decimal totalAmount = 0;
        foreach (var item in request.OrderItems)
        {
            var product = await catalogService.GetProductById(item.ProductId);
            if (product == null)
                throw new NotFoundException($"Product with ID {item.ProductId} not found");

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = product.Price
            };

            order.OrderItems.Add(orderItem);
            totalAmount += orderItem.UnitPrice * item.Quantity;
        }

        order.TotalAmount = totalAmount;
        await orderRepository.CreateAsync(order);

        return order.Id;
    }
} 