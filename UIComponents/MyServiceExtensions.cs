using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace UIComponents;

public static class MyServiceExtensions
{
    public static IHostApplicationBuilder AddMyServices(this IHostApplicationBuilder builder)
    {
        // There is some configuration dependency, example:
        // var configValue = builder.Configuration.GetValue<string>("MyServiceConfig");
        // if (configValue == ...) { ... }
        builder.Services.AddScoped<MyService>();
        return builder;
    }
}
