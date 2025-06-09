using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class AddToCartUseCase(IOrderService orderService)
{
    public async Task ExecuteAsync(AddToCartRequest request)
    {
        await orderService.AddToCartAsync(request);
    }
}
