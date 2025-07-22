using System.Reflection;
using Application.Common.Behaviors;
using Application.Common.Caching;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
   public static IServiceCollection AddApplication(this IServiceCollection services)
   {
      services.AddMediatR(config =>
      {
         config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
         config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
      });

      services.AddScoped(
          typeof(IPipelineBehavior<,>),
          typeof(ValidateCommandBehavior<,>)
      );

      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

      services.AddScoped<ICacheHelper, CacheHelper>();

      return services;
   }
}