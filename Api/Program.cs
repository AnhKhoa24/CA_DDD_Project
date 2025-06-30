using Api;
using Api.Common.Mapping;
using Application;
using Authentication;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
    // builder.Services.AddControllers(options => options.Filters.Add<ErrorHandingFilterAttribute>());
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandingMiddleware>();
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

