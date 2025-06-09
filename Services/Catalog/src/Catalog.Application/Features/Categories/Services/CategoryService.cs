using Catalog.Application.Features.Categories.Contracts.Requests;
using Catalog.Application.Features.Categories.Contracts.Responses;
using Catalog.Application.Features.Categories.Interfaces;
using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    // Gets a list of categories
    public async Task<GetCategoriesResponse> GetListAsync(GetCategoriesRequest request)
    {
        var categories = await categoryRepository.GetListAsync();
        var items = categories.Select(c => new GetCategoriesItemResponse
        {
            Id = c.Id,
            Name = c.Name,
            IconUrl = c.IconUrl,
            Color = c.Color
        });
        return new GetCategoriesResponse { Items = items };
    }

    // Gets a category 
    public async Task<GetCategoryResponse> GetAsync(GetCategoryRequest request)
    {
        var category = await categoryRepository.GetAsync(request.Id);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category with ID {request.Id} not found.");
        }
        var response = new GetCategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            IconUrl = category.IconUrl,
            Color = category.Color
        };
        return response;
    }

    // Creates a new category
    public async Task<CreateCategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        var category = new Category
        {
            Name = request.Name,
            IconUrl = request.IconUrl,
            Color = request.Color
        };
        var id = await categoryRepository.CreateAsync(category);
        return new CreateCategoryResponse { Id = id };
    }

    // Updates an existing category
    public async Task UpdateAsync(UpdateCategoryRequest request)
    {
        var category = new Category
        {
            Id = request.Id,
            Name = request.Name,
            IconUrl = request.IconUrl,
            Color = request.Color
        };
        await categoryRepository.UpdateAsync(category);
    }

    // Deletes a category
    public async Task<DeleteCategoryResponse> DeleteAsync(DeleteCategoryRequest request)
    {
        await categoryRepository.DeleteAsync(request.Id);
        return new DeleteCategoryResponse();
    }

}