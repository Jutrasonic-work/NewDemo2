using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class ClearCartUseCase(IOrderService orderService)
{
    public async Task ExecuteAsync()
    {
        await orderService.ClearCartAsync();
    }
}