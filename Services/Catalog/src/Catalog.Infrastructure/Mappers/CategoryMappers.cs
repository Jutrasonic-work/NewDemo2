using Catalog.Domain.Entities;
using Catalog.Infrastructure.Dao.Categories;

namespace Catalog.Infrastructure.Mappers;

public static class CategoryMappers
{
    public static Category Map(this GetCategoryDao dao)
    {
        return new Category
        {
            Id = dao.Id,
            Name = dao.Label,
            IconUrl = dao.Icon,
            Color = dao.Color
        };

    }
    public static Category Map(this GetCategoriesDao dao)
    {
        return new Category
        {
            Id = dao.Id,
            Name = dao.Label,
            IconUrl = dao.Icon,
            Color = dao.Color
        };
    }
    public static IEnumerable<Category> Map(this IEnumerable<GetCategoriesDao> daos)
    {
        return daos.Select(dao => dao.Map());
    }

}