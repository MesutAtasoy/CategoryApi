using AutoMapper;
using CategoryApi.Application.Products.Dto;
using CategoryApi.Domain.Entities;

namespace CategoryApi.Application.Products;

internal class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}