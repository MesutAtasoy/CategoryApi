using CategoryApi.Application.Categories.Dto;
using CategoryApi.Application.Categories.Dto.Request;

namespace CategoryApi.Application.Categories.Services;
public interface ICategoryService
{
    Task<CategoryDto> CreateAsync(CreateCategoryDto category);
    Task<CategoryDto> GetByIdAsync(Guid categoryId);
    Task<CategoryDto> UpdateAsync(Guid categoryId, UpdateCategoryDto category);
    Task DeleteAsync(Guid categoryId);
}
