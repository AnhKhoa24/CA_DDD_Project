using Application.Common.Cache.Interfaces;
using Application.Menus.Cache;
using Domain.Menu.Events;
using MediatR;

namespace Application.Menus.Events;

public class DummyHandler : INotificationHandler<MenuCreated>
{
   private readonly IGenericCacheService _cache;

   public DummyHandler(IGenericCacheService cache)
   {
      _cache = cache;
   }

   public async Task Handle(MenuCreated notification, CancellationToken cancellationToken)
   { 
      // throw new Exception("");
      await _cache.ClearCacheFromGroupKeyAsync(MenuCacheSettings.GetGroupKey(), cancellationToken);
      // return Task.CompletedTask;
   }
}
