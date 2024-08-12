using AutoMapper;
using ProductManager.Application.Commands;
using ProductManager.Application.Responses;
using ProductManager.Core.Entities;

namespace ProductManager.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
        }
    }
}
