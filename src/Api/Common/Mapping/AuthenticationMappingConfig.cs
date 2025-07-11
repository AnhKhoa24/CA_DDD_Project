using Application.Authentication.Commmon;
using Contractas.Authentication;
using Mapster;

namespace Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<AuthenticationResult, AuthenticationResponse>();
    }
}