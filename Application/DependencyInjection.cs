using System.Reflection;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidateCommandBehavior<,>)
        );

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}