using Application.Services.Authentication.Commmon;
using Contractas.Authentication;
using Mapster;

namespace Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.token)
            .Map(dest => dest, src => src.user);
    }
}