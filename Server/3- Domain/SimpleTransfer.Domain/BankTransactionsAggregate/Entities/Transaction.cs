using Flunt.Validations;
using SimpleTransfer.Common.Models;
using SimpleTransfer.Domain.BankTransactionsAggregate.Enums;

namespace SimpleTransfer.Domain.BankTransactionsAggregate.Entities;

public class Transaction : BaseEntity
{
    public decimal Value { get; private set; }
    public ETransactionType TransactionType { get; private set; }
    public string PayerId { get; private set; } = null!;
    public AccountBank Payer { get; private set; }
    public string PayeeId { get; private set; } = null!;
    public AccountBank Payee { get; private set; }

    private Transaction()
    {
    }

    private Transaction(decimal value, string payerId, string payeeId, ETransactionType transactionType)
    {
        Value = value;
        PayerId = payerId;
        PayeeId = payeeId;
        TransactionType = transactionType;
        Validate();
    }
    
    internal static Transaction Create(decimal value, string payerId, string payeeId, ETransactionType transactionType)
    {
        return new Transaction(value, payerId, payeeId, transactionType);
    }   
    
    public sealed override void Validate()
    {
        AddNotifications(
            new Contract<Transaction>()
                .Requires()
                .IsGreaterThan(Value, 0, nameof(Value), "Valor da transação deve ser maior que zero")
                .IsNotNullOrEmpty(PayerId, nameof(PayerId), "Conta pagadora inválida")
                .IsNotNullOrEmpty(PayeeId, nameof(PayeeId), "Conta recebedora inválida")
        );
    }
}