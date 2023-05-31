using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using Mapster;

namespace Presentation.API.Mapping;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, LoginResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Expiration, src => src.Expiration);

        config.NewConfig<RegsiterResult, RegisterResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Expiration, src => src.Expiration);

        config.NewConfig<RegisterRequest, RegisterCommand>()
            .Map(dest => dest.Customer, src => src.Customer);
    }
}
