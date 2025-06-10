using FrontEnd.Api.Features.Shop.Contracts.Requests;
using FrontEnd.Api.Services.Order;

namespace FrontEnd.Api.Features.Shop.UseCases;

public class CreateOrderUseCase(IOrderService orderService)
{
    public async Task<Guid> ExecuteAsync(CreateOrderRequest request)
    {
        return await orderService.CreateOrderAsync(request);
    }
} 