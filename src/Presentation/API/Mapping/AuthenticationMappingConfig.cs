using Application.Authentication.Common;
using Contracts.Authentication;
using Mapster;

namespace Presentation.API.Mapping;
public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Expiration, src => src.Expiration);
    }
}
