using FrontEnd.Api.Features.Shop.Contracts.Responses;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class GetOrderUseCase(IOrderService orderService)
{
    public async Task<OrderResponse> ExecuteAsync(int id)
    {
        return await orderService.GetOrderAsync(id);
    }
}
