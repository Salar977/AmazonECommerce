using AmazonECommerce.Application.DTOs.Cart;
using AmazonECommerce.Application.DTOs.Categories;
using AmazonECommerce.Application.DTOs.Identity;
using AmazonECommerce.Application.DTOs.Products;
using AmazonECommerce.Application.Identity;
using AmazonECommerce.Domain.Entities;
using AmazonECommerce.Domain.Entities.Cart;
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

        CreateMap<UserRequest, AppUser>();
        CreateMap<UserLogin, AppUser>();

        CreateMap<PaymentMethod, PaymentMethodResponse>();
        CreateMap<OrderRequest, Order>();

    }
}