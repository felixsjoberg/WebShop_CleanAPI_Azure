using Application.Products.Common.DTOs;
using Domain.Entities;
using Mapster;

namespace Presentation.API.Mapping;
public class GetAllProductsMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductDto>()
        .Map(dest => dest.Id, src => src.Id.Value);
    }
}
