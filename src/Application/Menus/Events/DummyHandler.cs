using Application.Menus.Cache;
using Domain.Menu.Events;
using MediatR;

namespace Application.Menus.Events;

public class DummyHandler : INotificationHandler<MenuCreated>
{

   public async Task Handle(MenuCreated notification, CancellationToken cancellationToken)
   { 
      // throw new Exception("");
      // await _cache.ClearCacheFromGroupKeyAsync(MenuCacheSettings.GetGroupKey(), cancellationToken);
      await Task.CompletedTask;
   }
}
