﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleTransfer.Data.Context;

#nullable disable

namespace SimpleTransfer.Data.Migrations
{
    [DbContext(typeof(SimpleTransferContext))]
    partial class SimpleTransferContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.AccountBank", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("Balance")
                        .HasColumnType("DECIMAL(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UsersBank");
                });

            modelBuilder.Entity("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.Transaction", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PayeeId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<string>("PayerId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(36)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("VARCHAR");

                    b.Property<decimal>("Value")
                        .HasColumnType("DECIMAL(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PayeeId");

                    b.HasIndex("PayerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("SimpleTransfer.Domain.IdentityAggregate.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AccountBankId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.AccountBank", b =>
                {
                    b.HasOne("SimpleTransfer.Domain.IdentityAggregate.Entities.User", "User")
                        .WithOne("AccountBank")
                        .HasForeignKey("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.AccountBank", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.Transaction", b =>
                {
                    b.HasOne("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.AccountBank", "Payee")
                        .WithMany("PayeeTransactions")
                        .HasForeignKey("PayeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.AccountBank", "Payer")
                        .WithMany("PayerTransactions")
                        .HasForeignKey("PayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Payee");

                    b.Navigation("Payer");
                });

            modelBuilder.Entity("SimpleTransfer.Domain.IdentityAggregate.Entities.User", b =>
                {
                    b.OwnsOne("SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory.Document", "Document", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("VARCHAR(36)");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(25)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("DocumentNumber");

                            b1.Property<string>("Type")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("VARCHAR")
                                .HasColumnName("DocumentType");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Document")
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleTransfer.Domain.BankTransactionsAggregate.Entities.AccountBank", b =>
                {
                    b.Navigation("PayeeTransactions");

                    b.Navigation("PayerTransactions");
                });

            modelBuilder.Entity("SimpleTransfer.Domain.IdentityAggregate.Entities.User", b =>
                {
                    b.Navigation("AccountBank")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
