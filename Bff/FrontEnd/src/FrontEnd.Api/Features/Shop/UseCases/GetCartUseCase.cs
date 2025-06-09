using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetCartUseCase(IOrderService orderService)
{
    public async Task<CartResponse> ExecuteAsync(int id)
    {
        return await orderService.GetCartAsync(id);
    }
}
