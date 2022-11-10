using AutoMapper;
using AutoMapper.QueryableExtensions;
using CategoryApi.Application.Products.Dto;
using CategoryApi.Application.Products.Dto.Request;
using CategoryApi.Application.Shared.Exceptions;
using CategoryApi.Application.Shared.Extensions;
using CategoryApi.Application.Shared.Models;
using CategoryApi.Domain.Entities;
using CategoryApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Application.Products.Services;

public class ProductService : IProductService
{
    private readonly CategoryApiDbContext _dbContext;
    private readonly IMapper _mapper;

    public ProductService(CategoryApiDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<PagedList<ProductDto>> GetAsync(long categoryId, PaginationFilter filter)
    {
        return await _dbContext.Products.Where(x => x.CategoryId == categoryId)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToPagedListAsync(filter);
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));

        var newProduct = new Product { Name = product.Name, CategoryId = product.CategoryId, Code = product.Code };

        await _dbContext.Products.AddAsync(newProduct);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductDto>(newProduct);
    }

    public async Task<ProductDto> GetByIdAsync(long productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == productId);
        if (product is null)
            throw new ItemNotFoundException($"Product is not found");

        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> UpdateAsync(long productId, UpdateProductDto product)
    {
        var updatedProduct = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == productId);
        if (updatedProduct is null)
            throw new ItemNotFoundException($"Product is not found");

        updatedProduct.Name = product.Name;
        updatedProduct.Code = product.Code;
        updatedProduct.CategoryId = product.CategoryId;
        
        await _dbContext.SaveChangesAsync();

        return _mapper.Map<ProductDto>(updatedProduct);
    }

    public async Task DeleteAsync(long productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == productId);
        if (product is null)
            throw new ItemNotFoundException($"Product is not found");
        
        _dbContext.Products.Remove(product);

        await _dbContext.SaveChangesAsync();
    }
}