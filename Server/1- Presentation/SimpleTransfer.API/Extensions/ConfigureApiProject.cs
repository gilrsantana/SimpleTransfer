namespace SimpleTransfer.API.Extensions;

public static class ConfigureApiProject
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddEndpointsApiExplorer();
    }
}