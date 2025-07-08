using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Authentication;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      ConfigurationManager configuration)
   {
      services
         .AddAuth(configuration)
         .AddPersistence(configuration);

      return services;
   }

   private static IServiceCollection AddPersistence(
      this IServiceCollection services,
      ConfigurationManager configuration)
   {
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IMenuRepository, MenuRepository>();
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