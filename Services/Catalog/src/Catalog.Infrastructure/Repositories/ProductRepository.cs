using Catalog.Application.Features.Products.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Infrastructure.Core.Sql;
using Catalog.Infrastructure.Dao.Products;
using Catalog.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository(ISqlFactory sqlFactory, IConfiguration configuration) : IProductRepository
{
    // Gets a list of products
    public async Task<IEnumerable<Product>> GetListAsync()
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var productsDao = await connection.QueryAsync<GetProductsDao>(
            sql: @"
                SELECT 
                    p.Id,
                    Name,
                    Description,
                    Price,
                    ImageUrl,
                    CategoryId,
                    CategoryName = c.Label,
                    CategoryIconUrl = c.Icon,
                    CategoryColor = c.Color,
                    Stock,
                    Unit,
                    InStock
                FROM Products p
                LEFT JOIN Categories c ON p.CategoryId = c.Id",
            commandType: CommandType.Text);
        return productsDao.Map();
    }

    // Gets product by id
    public async Task<Product> GetAsync(Guid id)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var productDao = await connection.QuerySingleAsync<GetProductDao>(
            sql: @"
                SELECT 
                    Id,
                    Name,
                    Description,
                    Price,
                    ImageUrl,
                    CategoryId,
                    Stock,
                    Unit,
                    InStock
                FROM Products
                WHERE Id = @Id",
            param: new { Id = id },
            commandType: CommandType.Text);
        return productDao.Map();
    }

    // Creates a new product
    public async Task<Guid> CreateAsync(Product product)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var id = Guid.NewGuid();
        var parameters = new DynamicParameters(
            new
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Stock = product.Stock,
                Unit = product.Unit
            });
        await connection.ExecuteAsync(
            sql: @"
                INSERT INTO Products (Id, Name, Description, Price, ImageUrl, CategoryId, Stock, Unit)
                VALUES (@Id, @Name, @Description, @Price, @ImageUrl, @CategoryId, @Stock, @Unit)",
            param: parameters,
            commandType: CommandType.Text);
        return id;
    }

    // Updates an existing product
    public async Task UpdateAsync(Product product)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var parameters = new DynamicParameters(
            new
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Stock = product.Stock,
                Unit = product.Unit
            });
        var rowsAffected = await connection.ExecuteAsync(
            sql: @"
                UPDATE Products 
                SET Name = @Name, Description = @Description, Price = @Price, ImageUrl = @ImageUrl, 
                    CategoryId = @Category, Stock = @Stock, Unit = @Unit
                WHERE Id = @Id",
            param: parameters,
            commandType: CommandType.Text);

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
        }
    }

    // Deletes a product by id
    public async Task DeleteAsync(Guid id)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var rowsAffected = await connection.ExecuteAsync(
            sql: @"
                DELETE FROM Products 
                WHERE Id = @Id",
            param: new { Id = id },
            commandType: CommandType.Text);
        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
    }

}