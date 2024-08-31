using Mapster;
using AtulaHomeFurniture.Models;

namespace AtulaHomeFurniture.Mapping
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Product, ProductDto>.NewConfig()
                .Map(dest => dest.CategoryIds, src => src.Categories.Select(c => c.Id));
            TypeAdapterConfig<ProductDto, Product>.NewConfig()
                .Map(dest => dest.Categories, src => src.CategoryIds.Select(id => new Category { Id = id }));
        }
    }
}

