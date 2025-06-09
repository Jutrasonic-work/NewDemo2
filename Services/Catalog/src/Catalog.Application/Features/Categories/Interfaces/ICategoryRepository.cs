using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Interfaces;

public interface ICategoryRepository
{
    /// <summary>
    /// Gets a list of categories.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Category>> GetListAsync();

    /// <summary>
    /// Gets a category by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Category> GetAsync(Guid id);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    Task<Guid> CreateAsync(Category category);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    Task UpdateAsync(Category category);

    /// <summary>
    /// Deletes a category by ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);
}
