using SimpleTransfer.Persistence.Extensions;

namespace SimpleTransfer.API.Extensions;

public static class ConfigurePersistenceProject
{
    public static void ConfigurePersistence(this IServiceCollection services)
    {
        services.Configure();
    }
}