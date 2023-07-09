using AutoMapper;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.Dto;

namespace Mango.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<ProductDto, Product>().ReverseMap();
                    });
            return mappingConfig;
        }
    }
}
