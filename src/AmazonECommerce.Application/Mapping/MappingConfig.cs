using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Domain.Entities;
using AutoMapper;

namespace AmazonECommerce.Application.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CategoryRequest, Category>();
        CreateMap<CategoryUpdate, Category>();
        CreateMap<Category, CategoryResponse>();

        CreateMap<ProductRequest, Product>();
        CreateMap<ProductUpdate, Product>();
        CreateMap<Product, ProductResponse>();

    }
}