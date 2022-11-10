using CategoryApi.Application.Products.Dto;
using CategoryApi.Application.Products.Dto.Request;
using CategoryApi.Application.Shared.Models;

namespace CategoryApi.Application.Products.Services;

public interface IProductService
{
    Task<PagedList<ProductDto>> GetAsync(long categoryId, PaginationFilter filter);
    Task<ProductDto> CreateAsync(CreateProductDto product);
    Task<ProductDto> GetByIdAsync(long productId);
    Task<ProductDto> UpdateAsync(long productId, UpdateProductDto product);
    Task DeleteAsync(long productId);
}