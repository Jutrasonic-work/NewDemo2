using Catalog.Application.Features.Categories.Contracts.Requests;
using Catalog.Application.Features.Categories.Contracts.Responses;

namespace Catalog.Application.Features.Categories.Interfaces;
public interface ICategoryService
{
    /// <summary>
    /// Gets a list of categories with optional filtering.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetCategoriesResponse> GetListAsync(GetCategoriesRequest request);

    /// <summary>
    /// Gets a category.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetCategoryResponse> GetAsync(GetCategoryRequest request);

    /// <summary>
    /// Creates a new category.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<CreateCategoryResponse> CreateAsync(CreateCategoryRequest request);

    /// <summary>
    /// Updates an existing category.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task UpdateAsync(UpdateCategoryRequest request);

    /// <summary>
    /// Deletes a category.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<DeleteCategoryResponse> DeleteAsync(DeleteCategoryRequest request);
}
