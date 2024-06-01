using Flunt.Validations;
using SimpleTransfer.Common.Models;
using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Domain.BankTransactionsAggregate.Entities;

public class AccountBank : BaseEntity
{
    public decimal Balance { get; private set; }
    public string UserId { get; private set; }
    public User User { get; private set; }
    public ICollection<Transaction> PayerTransactions { get; set; }
    public ICollection<Transaction> PayeeTransactions { get; set; }

    private AccountBank()
    {
    }
    
    private AccountBank(User user)
    {
        User= user;
        Validate();
    }
    
    public static AccountBank Create(User user)
    {
        return new AccountBank(user);
    }
    
    internal void UpdateBalance(decimal value)
    {
        Balance += value;
    }
    
    public sealed override void Validate()
    {
        AddNotifications(
            new Contract<AccountBank>()
                .Requires()
                .IsTrue(User.IsValid, nameof(User), "Usuário inválido")
        );
    }

}