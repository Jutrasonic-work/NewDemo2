using Catalog.Application.Features.Categories.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Catalog.Infrastructure.Core.Sql;
using Catalog.Infrastructure.Dao.Categories;
using Catalog.Infrastructure.Mappers;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure.Repositories;

public class CategoryRepository(ISqlFactory sqlFactory, IConfiguration configuration)
    : ICategoryRepository
{

    // Gets a list of categories
    public async Task<IEnumerable<Category>> GetListAsync()
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var categoriesDao = await connection.QueryAsync<GetCategoriesDao>(
              sql: @"
                    SELECT 
                        Id,
                        Label,
                        Icon,
                        Color
                    FROM Categories",
              commandType: CommandType.Text);
        return categoriesDao.Map();
    }

    // Gets category by id
    public async Task<Category> GetAsync(Guid id)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var categoryDao = await connection.QuerySingleAsync<GetCategoryDao>(
              sql: @"
                    SELECT 
                        Id,
                        Label,
                        Icon,
                        Color
                    FROM Categories
                    WHERE Id = @Id",
              param: new { Id = id },
              commandType: CommandType.Text);
        return categoryDao.Map();
    }

    // Creates a new category
    public async Task<Guid> CreateAsync(Category category)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);

        var id = Guid.NewGuid();
        var parameters = new DynamicParameters(
            new
            {
                Id = id,
                Label = category.Name,
                Icon = category.IconUrl,
                Color = category.Color
            });
        var sql = @"
            INSERT INTO Categories (Id, Label, Icon, Color)
            VALUES (@Id, @Label, @Icon, @Color)";

        await connection.ExecuteAsync(
            sql: sql,
            param: parameters,
            commandType: CommandType.Text);
        return id;
    }

    // Updates an existing category
    public async Task UpdateAsync(Category category)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var rowsAffected = await connection.ExecuteAsync(
            sql: @"
                UPDATE Categories
                SET Label = @Label, Icon = @Icon, Color = @Color
                WHERE Id = @Id",
            param: new
            {
                Id = category.Id,
                Label = category.Name,
                Icon = category.IconUrl,
                Color = category.Color
            },
            commandType: CommandType.Text);

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Category with ID {category.Id} not found.");
        }
    }

    // Deletes a category by id
    public async Task DeleteAsync(Guid id)
    {
        using var connection = sqlFactory.CreateConnection(configuration.GetConnectionString(ConnectionStrings.Demo)!);
        var rowsAffected = await connection.ExecuteAsync(
            sql: @"
                DELETE FROM Categories
                WHERE Id = @Id",
            param: new { Id = id },
            commandType: CommandType.Text);

        if (rowsAffected == 0)
        {
            throw new KeyNotFoundException($"Category with ID {id} not found.");
        }
    }

}