using Domain.Entities;
using Mapster;

namespace Presentation.API.Mapping;
public class GetAllCustomersMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerDto>()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.FullName, src => src.FullName)
        .Map(dest => dest.AddressId, src => src.AddressId)
        .Map(dest => dest.UserId, src => src.UserId);
    }
}
