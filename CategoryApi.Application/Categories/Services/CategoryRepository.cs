using CategoryApi.Application.Categories.Dto;
using CategoryApi.Application.Categories.Dto.Request;
using CategoryApi.Application.Shared.Exceptions;
using CategoryApi.Domain.Entities;
using CategoryApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Application.Categories.Services;

public class CategoryRepository : ICategoryRepository
{
    private readonly CategoryApiDbContext _dbContext;

    public CategoryRepository(CategoryApiDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<List<CategoryDto>> GetAsync()
    {
        return await _dbContext.Categories
            .Select(x=> new CategoryDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync(); 
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto category)
    {
        if (category is null)
            throw new ArgumentNullException(nameof(category));

        var newCategory = new Category { Name = category.Name };

        await _dbContext.Categories.AddAsync(newCategory);

        await _dbContext.SaveChangesAsync();

        return new CategoryDto
        {
            Id = newCategory.Id,
            Name = newCategory.Name
        };
    }

    public async Task<CategoryDto> GetByIdAsync(long categoryId)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category is null)
            throw new ItemNotFoundException($"Category is not found");

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<CategoryDto> UpdateAsync(long categoryId, UpdateCategoryDto category)
    {
        var updatedCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (updatedCategory is null)
            throw new ItemNotFoundException($"Category is not found");

        updatedCategory.Name = category.Name;

        await _dbContext.SaveChangesAsync();

        return new CategoryDto
        {
            Id = updatedCategory.Id,
            Name = updatedCategory.Name
        };
    }

    public async Task DeleteAsync(long categoryId)
    {
        var category = await _dbContext.Categories
            .Include(x=>x.Products)
            .FirstOrDefaultAsync(c => c.Id == categoryId);
        
        if (category is null)
            throw new ItemNotFoundException($"Category is not found");

        _dbContext.Categories.Remove(category);

        if (category.Products.Any())
        {
            _dbContext.Products.RemoveRange(category.Products);
        }
        
        await _dbContext.SaveChangesAsync();
    }
}