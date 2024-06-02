using Microsoft.Extensions.DependencyInjection;
using SimpleTransfer.Application.Identity.Token.Interface;
using SimpleTransfer.Application.Identity.Token.Service;

namespace SimpleTransfer.Application.Extensions;

public static class ApplicationExtension
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
    }
}