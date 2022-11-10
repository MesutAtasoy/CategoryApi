using AutoMapper;
using CategoryApi.Application.Categories.Dto;
using CategoryApi.Application.Categories.Dto.Request;
using CategoryApi.Domain.Entities;
using CategoryApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Application.Categories.Services;

public class CategoryService : ICategoryService
{
    private readonly CategoryApiDbContext _dbContext;
    private readonly IMapper _mapper;

    public CategoryService(CategoryApiDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto category)
    {
        if (category is null)
            throw new ArgumentNullException(nameof(category));

        var newCategory = new Category { Name = category.Name };

        await _dbContext.Categories.AddAsync(newCategory);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(newCategory);
    }

    public async Task<CategoryDto> GetByIdAsync(long categoryId)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category is null)
            throw new ArgumentException($"Category is not found"); // ToDo : Create new exception type


        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> UpdateAsync(long categoryId, UpdateCategoryDto category)
    {
        var updatedCategory = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (updatedCategory is null)
            throw new ArgumentException($"Category is not found"); // ToDo : Create new exception type

        updatedCategory.Name = category.Name;

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<CategoryDto>(updatedCategory);
    }

    public async Task DeleteAsync(long categoryId)
    {
        var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        if (category is null)
            throw new ArgumentException($"Category is not found"); // ToDo : Create new exception type


        _dbContext.Categories.Remove(category);

        await _dbContext.SaveChangesAsync();
    }
}