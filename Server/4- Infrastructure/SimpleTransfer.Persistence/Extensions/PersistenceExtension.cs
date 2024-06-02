using Microsoft.Extensions.DependencyInjection;
using SimpleTransfer.Common.Interfaces;
using SimpleTransfer.Domain.BankAggregate.Interfaces;
using SimpleTransfer.Domain.IdentityAggregate.Interfaces;
using SimpleTransfer.Persistence.Common;
using SimpleTransfer.Persistence.Domain;

namespace SimpleTransfer.Persistence.Extensions;

public static class PersistenceExtension
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountBankRepository, AccountBankRepository>();
    }
}