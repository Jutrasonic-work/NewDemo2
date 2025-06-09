using Catalog.Domain.Entities;
using Catalog.Infrastructure.Dao.Products;

namespace Catalog.Infrastructure.Mappers;

public static class ProductMappers
{
    public static Product Map(this GetProductDao dao)
    {
        return new Product
        {
            Id = dao.Id,
            Name = dao.Name,
            Description = dao.Description,
            Price = dao.Price,
            ImageUrl = dao.ImageUrl,
            CategoryId = dao.CategoryId,
            Stock = dao.Stock,
            Unit = dao.Unit,
            InStock = dao.Stock > 0
        };
    }
    public static Product Map(this GetProductsDao dao)
    {
        return new Product
        {
            Id = dao.Id,
            Name = dao.Name,
            Description = dao.Description,
            Price = dao.Price,
            ImageUrl = dao.ImageUrl,
            Category = new Category
            {
                Id = dao.CategoryId,
                Name = dao.CategoryName,
                Color = dao.CategoryColor,
                IconUrl = dao.CategoryIconUrl
            },
            Unit = dao.Unit,
            InStock = dao.InStock
        };
    }
    public static IEnumerable<Product> Map(this IEnumerable<GetProductsDao> daos)
    {
        return daos.Select(dao => dao.Map());
    }


}
