using AutoMapper;
using Vietast.Services.ProductAPI.Models;
using Vietast.Services.ProductAPI.Models.DTO;

namespace Vietast.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDTO>().ReverseMap();
                config.CreateMap<ProductCreateDTO, Product>().ReverseMap();
                config.CreateMap<ProductUpdateDTO, Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                config.CreateMap<Category, CategoryDTO>().ReverseMap();
                config.CreateMap<CategoryCreateDTO, Category>().ReverseMap();
                config.CreateMap<CategoryUpdateDTO, Category>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                config.CreateMap<SKU, SKUDTO>().ReverseMap();
                config.CreateMap<SKUCreateDTO, SKU>().ReverseMap();
                config.CreateMap<SKUUpdateDTO, SKU>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
