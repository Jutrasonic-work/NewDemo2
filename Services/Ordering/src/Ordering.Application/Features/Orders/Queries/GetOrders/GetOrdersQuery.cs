using MediatR;
using System.Globalization;

namespace Ordering.Application.Features.Orders.Queries.GetOrders;

public record GetOrdersQuery : IRequest<IEnumerable<OrderDto>>
{
    public GetOrdersParameters Parameters { get; init; } = new();
}

public record GetOrdersParameters : IParsable<GetOrdersParameters>
{
    public Guid? UserId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }

    public static GetOrdersParameters Parse(string s, IFormatProvider? provider) => 
        TryParse(s, provider, out var result) ? result : throw new FormatException("Invalid format");

    public static bool TryParse(string? s, IFormatProvider? provider, out GetOrdersParameters result)
    {
        result = new GetOrdersParameters();
        if (string.IsNullOrEmpty(s)) return true;

        var parts = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in parts)
        {
            var keyValue = part.Split('=');
            if (keyValue.Length != 2) continue;

            var key = keyValue[0].Trim().ToLower();
            var value = keyValue[1].Trim();

            switch (key)
            {
                case "userid" when Guid.TryParse(value, out var userId):
                    result = result with { UserId = userId };
                    break;
                case "fromdate" when DateTime.TryParse(value, provider, DateTimeStyles.None, out var fromDate):
                    result = result with { FromDate = fromDate };
                    break;
                case "todate" when DateTime.TryParse(value, provider, DateTimeStyles.None, out var toDate):
                    result = result with { ToDate = toDate };
                    break;
            }
        }
        return true;
    }
}

public record OrderDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public DateTime OrderDate { get; init; }
    public decimal TotalAmount { get; init; }
    public string ShippingAddress { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public List<OrderItemDto> OrderItems { get; init; } = new();
}

public record OrderItemDto
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
} 