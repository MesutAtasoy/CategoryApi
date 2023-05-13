using CategoryApi.Application.Categories.Dto;
using CategoryApi.Application.Categories.Dto.Request;

namespace CategoryApi.Application.Categories.Services;
public interface ICategoryRepository
{
    Task<List<CategoryDto>> GetAsync();
    Task<CategoryDto> CreateAsync(CreateCategoryDto category);
    Task<CategoryDto> GetByIdAsync(long categoryId);
    Task<CategoryDto> UpdateAsync(long categoryId, UpdateCategoryDto category);
    Task DeleteAsync(long categoryId);
}
