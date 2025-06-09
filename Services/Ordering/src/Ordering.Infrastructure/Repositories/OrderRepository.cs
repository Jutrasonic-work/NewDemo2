using System.Data;
using Dapper;
using Ordering.Domain.Entities;
using Ordering.Domain.Repositories;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(IDbConnection connection) : IOrderRepository
{
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        const string sql = @"
            SELECT o.*, oi.*
            FROM Orders o
            LEFT JOIN OrderItems oi ON o.Id = oi.OrderId
            WHERE o.Id = @Id";

        var orderDictionary = new Dictionary<Guid, Order>();
        
        await connection.QueryAsync<Order, OrderItem, Order>(
            sql,
            (order, orderItem) =>
            {
                if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                {
                    orderEntry = order;
                    orderEntry.OrderItems = new List<OrderItem>();
                    orderDictionary.Add(order.Id, orderEntry);
                }

                if (orderItem != null)
                    orderEntry.OrderItems.Add(orderItem);

                return orderEntry;
            },
            new { Id = id },
            splitOn: "Id");

        return orderDictionary.Values.FirstOrDefault();
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var sql = @"
            SELECT o.*, oi.*
            FROM Orders o
            LEFT JOIN OrderItems oi ON o.Id = oi.OrderId";

        var parameters = new DynamicParameters();


        var orderDictionary = new Dictionary<Guid, Order>();

        await connection.QueryAsync<Order, OrderItem, Order>(
            sql,
            (order, orderItem) =>
            {
                if (!orderDictionary.TryGetValue(order.Id, out var orderEntry))
                {
                    orderEntry = order;
                    orderEntry.OrderItems = new List<OrderItem>();
                    orderDictionary.Add(order.Id, orderEntry);
                }

                if (orderItem != null)
                    orderEntry.OrderItems.Add(orderItem);

                return orderEntry;
            },
            parameters,
            splitOn: "Id");

        return orderDictionary.Values;
    }

    public async Task<Guid> CreateAsync(Order order)
    {
        const string orderSql = @"
            INSERT INTO Orders (Id, UserId, OrderDate, TotalAmount, ShippingAddress, PhoneNumber)
            VALUES (@Id, @UserId, @OrderDate, @TotalAmount, @ShippingAddress, @PhoneNumber)";

        const string orderItemSql = @"
            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice)
            VALUES (@Id, @OrderId, @ProductId, @Quantity, @UnitPrice)";

        using var transaction = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync(orderSql, order, transaction);

            foreach (var item in order.OrderItems)
            {
                await connection.ExecuteAsync(orderItemSql, item, transaction);
            }

            transaction.Commit();
            return order.Id;
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task UpdateAsync(Order order)
    {
        const string updateOrderSql = @"
            UPDATE Orders 
            SET UserId = @UserId, OrderDate = @OrderDate, TotalAmount = @TotalAmount, 
                ShippingAddress = @ShippingAddress, PhoneNumber = @PhoneNumber
            WHERE Id = @Id";
        const string deleteOrderItemsSql = @"
            DELETE FROM OrderItems 
            WHERE OrderId = @Id";
        const string insertOrderItemSql = @"
            INSERT INTO OrderItems (Id, OrderId, ProductId, Quantity, UnitPrice)
            VALUES (@Id, @OrderId, @ProductId, @Quantity, @UnitPrice)";
        using var transaction = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync(updateOrderSql, order, transaction);
            await connection.ExecuteAsync(deleteOrderItemsSql, new { Id = order.Id }, transaction);
            foreach (var item in order.OrderItems)
            {
                await connection.ExecuteAsync(insertOrderItemSql, item, transaction);
            }
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        const string deleteOrderItemsSql = @"
            DELETE FROM OrderItems 
            WHERE OrderId = @Id";

        const string deleteOrderSql = @"
            DELETE FROM Orders 
            WHERE Id = @Id";

        using var transaction = connection.BeginTransaction();
        try
        {
            await connection.ExecuteAsync(deleteOrderItemsSql, new { Id = id }, transaction);
            await connection.ExecuteAsync(deleteOrderSql, new { Id = id }, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
} 