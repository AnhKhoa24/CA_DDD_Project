using Application.Services.Authentication;
using Authentication;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
    builder.Services
    .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

