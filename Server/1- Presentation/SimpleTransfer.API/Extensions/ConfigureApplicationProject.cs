using SimpleTransfer.Application.Extensions;

namespace SimpleTransfer.API.Extensions;

public static class ConfigureApplicationProject
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.Configure();
    }
}