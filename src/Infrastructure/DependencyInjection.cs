using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Authentication;
using Infrastructure.Cache;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Application.Common.Caching;

namespace Authentication;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      ConfigurationManager configuration)
   {
      services
         .AddAuth(configuration)
         .AddPersistence(configuration)
         .AddCache(configuration);

      return services;
   }

   private static IServiceCollection AddCache(this IServiceCollection services, ConfigurationManager configuration)
   {
      services.AddMemoryCache();
      services.AddScoped<ICacheService, CacheService>();
      
      var cacheSettings = new CacheServiceSetting();
      configuration.Bind(CacheServiceSetting.SectionName, cacheSettings);

      services.AddStackExchangeRedisCache(options =>
      {
         options.Configuration = cacheSettings.Configuration;
         options.InstanceName = cacheSettings.InstanceName;
      });
      services.AddScoped<ICacheProvider, RedisCacheProvider>();
      return services;
   }

   private static IServiceCollection AddPersistence(
      this IServiceCollection services,
      ConfigurationManager configuration)
   {
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IMenuRepository, MenuRepository>();
      services.AddScoped<PublishDomainEventsInterceptor>();

      services.AddDbContext<KhoaDinnerDbContext>(options =>
         options.UseSqlServer(configuration.GetConnectionString(DatabaseConnection.ConnectionString)));
      return services;
   }

   private static IServiceCollection AddAuth(
      this IServiceCollection services,
      ConfigurationManager configuration)
   {
      var jwtSettings = new JwtSettings();
      configuration.Bind(JwtSettings.SectionName, jwtSettings);

      services.AddSingleton(Options.Create(jwtSettings));
      services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

      services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
            options.TokenValidationParameters = new TokenValidationParameters
            {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = jwtSettings.Issuer,
               ValidAudience = jwtSettings.Audience,
               IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes(jwtSettings.Secret))
            };
         });
      return services;
   }
}