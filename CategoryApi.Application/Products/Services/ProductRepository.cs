using CategoryApi.Application.Products.Dto;
using CategoryApi.Application.Products.Dto.Request;
using CategoryApi.Application.Shared.Exceptions;
using CategoryApi.Domain.Entities;
using CategoryApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Application.Products.Services;

public class ProductRepository : IProductRepository
{
    private readonly CategoryApiDbContext _dbContext;

    public ProductRepository(CategoryApiDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<ProductDto>> GetAsync(long categoryId)
    {
        return await _dbContext.Products.Where(x => x.CategoryId == categoryId)
            .Select(x=> new ProductDto
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Code = x.Code,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<ProductDto> CreateAsync(CreateProductDto product)
    {
        if (product is null)
            throw new ArgumentNullException(nameof(product));

        var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == product.CategoryId);
        if (category is null)
            throw new ItemNotFoundException("The category is not found");

        var newProduct = new Product { Name = product.Name, CategoryId = product.CategoryId, Code = product.Code };

        await _dbContext.Products.AddAsync(newProduct);

        await _dbContext.SaveChangesAsync();

        return new ProductDto
        {
            Id = newProduct.Id,
            CategoryId = newProduct.CategoryId,
            Code = newProduct.Code,
            Name = newProduct.Name
        };
    }

    public async Task<ProductDto> GetByIdAsync(long productId)
    {
        var product = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == productId);
        if (product is null)
            throw new ItemNotFoundException($"Product is not found");

        return new ProductDto
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Code = product.Code,
            Name = product.Name
        };
    }

    public async Task<ProductDto> UpdateAsync(long productId, UpdateProductDto product)
    {
        var updatedProduct = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == productId);
        if (updatedProduct is null)
            throw new ItemNotFoundException($"Product is not found");
        
        updatedProduct.Name = product.Name;
        updatedProduct.Code = product.Code;
        
        await _dbContext.SaveChangesAsync();

        return new ProductDto
        {
            Id = updatedProduct.Id,
            CategoryId = updatedProduct.CategoryId,
            Code = updatedProduct.Code,
            Name = updatedProduct.Name
        };
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