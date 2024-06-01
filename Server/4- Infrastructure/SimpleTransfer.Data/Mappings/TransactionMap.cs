using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTransfer.Domain.BankTransactionsAggregate.Entities;
using SimpleTransfer.Domain.BankTransactionsAggregate.Enums;

namespace SimpleTransfer.Data.Mappings;

public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("VARCHAR")
            .HasMaxLength(36);
        builder.Property(x => x.Value)
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();
        builder.Property(x =>  x.TransactionType)
            .HasColumnType("VARCHAR")
            .HasMaxLength(50)
            .HasConversion(
                v => v.ToString(),
                v => (ETransactionType)Enum.Parse(typeof(ETransactionType), v))
            .IsRequired();
        builder.HasOne(x => x.Payer)
            .WithMany(x => x.PayerTransactions)
            .HasForeignKey(x => x.PayerId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Payee)
            .WithMany(x => x.PayeeTransactions)
            .HasForeignKey(x => x.PayeeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion()
            .IsConcurrencyToken();
        builder.Ignore(x => x.Notifications);
    }
}