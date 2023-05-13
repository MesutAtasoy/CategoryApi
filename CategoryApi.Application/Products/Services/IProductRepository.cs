using CategoryApi.Application.Products.Dto;
using CategoryApi.Application.Products.Dto.Request;

namespace CategoryApi.Application.Products.Services;

public interface IProductRepository
{
    Task<List<ProductDto>> GetAsync(long categoryId);
    Task<ProductDto> CreateAsync(CreateProductDto product);
    Task<ProductDto> GetByIdAsync(long productId);
    Task<ProductDto> UpdateAsync(long productId, UpdateProductDto product);
    Task DeleteAsync(long productId);
}