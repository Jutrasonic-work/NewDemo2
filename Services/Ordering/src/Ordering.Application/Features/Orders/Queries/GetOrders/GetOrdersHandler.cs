using MediatR;
using Ordering.Domain.Repositories;

namespace Ordering.Application.Features.Orders.Queries.GetOrders;

public class GetOrdersHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    public async Task<IEnumerable<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync();
        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserId = o.UserId,
            OrderDate = o.OrderDate,
            TotalAmount = o.TotalAmount,
            ShippingAddress = o.ShippingAddress,
            PhoneNumber = o.PhoneNumber,
            OrderItems = o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice
            }).ToList()
        });
    }
} 