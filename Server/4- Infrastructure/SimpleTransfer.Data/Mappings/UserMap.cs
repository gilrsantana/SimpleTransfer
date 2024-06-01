using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleTransfer.Domain.BankTransactionsAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.Enums;

namespace SimpleTransfer.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasColumnType("VARCHAR")
            .HasMaxLength(36);
        builder.OwnsOne(x => x.Document, y =>
        {
            y.Property(x => x.Number)
                .HasColumnName("DocumentNumber")
                .HasColumnType("VARCHAR")
                .HasMaxLength(25)
                .IsRequired();
            y.Property(x => x.Type)
                .HasColumnName("DocumentType")
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .HasConversion(
                    v => v.ToString(),
                    v => (EDocumentType)Enum.Parse(typeof(EDocumentType), v))
                .IsRequired();
            y.Ignore(x => x.Notifications);
            y.HasIndex(x => new {x.Number, x.Type})
                .IsUnique();
        });
        builder.HasOne(x => x.AccountBank)
            .WithOne(x => x.User)
            .HasForeignKey<AccountBank>(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.Ignore(x => x.Notifications);
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}