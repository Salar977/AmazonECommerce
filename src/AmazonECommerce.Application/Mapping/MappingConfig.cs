using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Domain.Entities;
using AutoMapper;

namespace AmazonECommerce.Application.Mapping;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<CreateCategory, Category>();
        CreateMap<UpdateCategory, Category>();
        CreateMap<Category, CategoryResponse>();

        CreateMap<CreateProduct, Product>();
        CreateMap<UpdateProduct, Product>();
        CreateMap<Product, ProductResponse>();

    }
}