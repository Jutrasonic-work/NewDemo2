using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class AddToOrderUseCase(IOrderService orderService)
{
    public async Task ExecuteAsync(int orderId, AddToOrderRequest request)
    {
        await orderService.AddToOrderAsync(orderId, request);
    }
} 