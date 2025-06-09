using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class UpdateCartItemUseCase(IOrderService orderService)
{
    public async Task ExecuteAsync(int id, UpdateCartItemRequest request)
    {
        await orderService.UpdateCartItemAsync(id, request);
    }
}
