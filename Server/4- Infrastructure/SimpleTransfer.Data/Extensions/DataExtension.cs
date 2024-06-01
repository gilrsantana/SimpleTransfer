using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Data.Extensions;

public static class DataExtension
{
    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context.SimpleTransferContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }
    
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<User>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
        })
        .AddSignInManager<SignInManager<User>>()
        .AddEntityFrameworkStores<Context.SimpleTransferContext>()
        .AddDefaultTokenProviders();
    }
}