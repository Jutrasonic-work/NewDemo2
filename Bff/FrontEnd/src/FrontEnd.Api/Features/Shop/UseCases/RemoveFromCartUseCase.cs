using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class RemoveFromCartUseCase(IOrderService orderService)
{
    public async Task ExecuteAsync(int id)
    {
        await orderService.RemoveFromCartAsync(id);
    }
}