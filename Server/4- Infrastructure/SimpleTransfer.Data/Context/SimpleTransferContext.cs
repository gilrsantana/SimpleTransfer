using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleTransfer.Domain.BankTransactionsAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Data.Context;

public class SimpleTransferContext : IdentityDbContext<User>
{
    public DbSet<AccountBank> UsersBank { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    public SimpleTransferContext(DbContextOptions<SimpleTransferContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<IdentityRole>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleTransferContext).Assembly);
    }
}