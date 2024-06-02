using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTransfer.Domain.BankAggregate.Entities;

namespace SimpleTransfer.Data.Mappings;

public class AccountBankMap : IEntityTypeConfiguration<AccountBank>
{
    public void Configure(EntityTypeBuilder<AccountBank> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("VARCHAR")
            .HasMaxLength(36);
        builder.Property(x => x.Balance)
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion()
            .IsConcurrencyToken();
        builder.Ignore(x => x.Notifications);
    }
}