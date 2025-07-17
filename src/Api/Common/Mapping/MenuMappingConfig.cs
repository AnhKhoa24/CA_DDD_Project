using Application.Menus;
using Application.Menus.Commands.UpdateMenu;
using Contracts.Menus;
using Mapster;

namespace Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
   public void Register(TypeAdapterConfig config)
   {
      config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
         .Map(dest => dest.HostId, src => src.HostId)
         .Map(dest => dest, src => src.Request);

      config.NewConfig<(UpdateMenuRequest Request, string HostId), UpdateMenuCommand>()
         .Map(dest => dest.HostId, src => src.HostId)
         .Map(dest => dest, src => src.Request);

   }
}
