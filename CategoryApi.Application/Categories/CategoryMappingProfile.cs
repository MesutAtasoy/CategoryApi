using AutoMapper;
using CategoryApi.Application.Categories.Dto;
using CategoryApi.Domain.Entities;

namespace CategoryApi.Application.Categories;

internal class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>();
    }
}