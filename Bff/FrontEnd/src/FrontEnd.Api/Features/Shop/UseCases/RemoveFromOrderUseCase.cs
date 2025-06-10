using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class RemoveFromOrderUseCase(IOrderService orderService)
{
    public async Task ExecuteAsync(int orderId)
    {
        await orderService.DeleteOrderAsync(orderId);
    }
}