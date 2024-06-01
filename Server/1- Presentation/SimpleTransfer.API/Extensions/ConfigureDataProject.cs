using SimpleTransfer.Data.Extensions;

namespace SimpleTransfer.API.Extensions;

public static class ConfigureDataProject
{
    public static void ConfigureData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext(configuration);
        services.ConfigureIdentity();
    }
}