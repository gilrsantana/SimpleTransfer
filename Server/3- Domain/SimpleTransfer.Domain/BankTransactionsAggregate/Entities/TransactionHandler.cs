using Flunt.Validations;
using SimpleTransfer.Common.Models;
using SimpleTransfer.Domain.BankTransactionsAggregate.Enums;
using SimpleTransfer.Domain.IdentityAggregate.Enums;

namespace SimpleTransfer.Domain.BankTransactionsAggregate.Entities;

public class TransactionHandler : BaseEntity
{
    public decimal Value { get; private set; }
    public AccountBank Payer { get; private set; }
    public AccountBank Payee { get; private set; }
    public Transaction? Transaction { get; private set; }
    
    public TransactionHandler(decimal value, AccountBank payer, AccountBank payee)
    {
        Value = value;
        Payer = payer;
        Payee = payee;
        
        Validate();
    }

    public void MakeTransfer()
    {
        if (!IsValidForTransfer())
            return;
        
        Payer.UpdateBalance(-Value);
        Payee.UpdateBalance(Value);
        
        Transaction = Transaction.Create(Value, Payer.Id, Payee.Id, ETransactionType.Transfer);
    }

    private bool IsValidForTransfer()
    {
        if (!IsValid)
            return false;
        
        if (string.IsNullOrEmpty(Payer.Id) || string.IsNullOrEmpty(Payee.Id))
        {
            AddNotification(nameof(Payer), "Conta inválida");
            return false;
        }
        
        if (Payer.Id.Equals(Payee.Id))
        {
            AddNotification(nameof(Payer), "Não é possível transferir para a mesma conta");
            return false;
        }
        
        if (Payer.Balance < Value)
        {
            AddNotification(nameof(Payer), "Saldo insuficiente");
            return false;
        }

        if (Payer.User.Document.Type == EDocumentType.CNPJ)
        {
            AddNotification(nameof(Payer), "Pessoa jurídica não pode realizar transferências");
            return false;
        }

        return true;
    }
    
    public void MakeDeposit()
    {
        if (!IsValidForDeposit())
            return;
        
        Payee.UpdateBalance(Value);
        Transaction = Transaction.Create(Value, Payer.Id, Payee.Id, ETransactionType.Deposit);
    }
    
    private bool IsValidForDeposit()
    {
        if (!IsValid)
            return false;
        
        if (string.IsNullOrEmpty(Payer.Id) || string.IsNullOrEmpty(Payee.Id))
        {
            AddNotification(nameof(Payer), "Conta inválida");
            return false;
        }
        
        if (!Payer.Id.Equals(Payee.Id))
        {
            AddNotification(nameof(Payer), "Operação não permitida");
            return false;
        }

        return true;
    }
    
    public void MakeWithdraw()
    {
        if (!IsValidForWithdraw())
            return;
        
        Payer.UpdateBalance(-Value);
        Transaction = Transaction.Create(Value, Payer.Id, Payee.Id, ETransactionType.Withdraw);
    }
    
    private bool IsValidForWithdraw()
    {
        if (!IsValid)
            return false;
        
        if (string.IsNullOrEmpty(Payer.Id) || string.IsNullOrEmpty(Payee.Id))
        {
            AddNotification(nameof(Payer), "Conta inválida");
            return false;
        }
        
        if (!Payer.Id.Equals(Payee.Id))
        {
            AddNotification(nameof(Payer), "Operação não permitida");
            return false;
        }
        
        if (Payer.Balance < Value)
        {
            AddNotification(nameof(Payer), "Saldo insuficiente");
            return false;
        }

        return true;
    }
    
    
    public sealed override void Validate()
    {
        AddNotifications(Payer);
        AddNotifications(Payee);
        AddNotifications(
            new Contract<TransactionHandler>()
                .Requires()
                .IsGreaterThan(Value, 0, nameof(Value), "Valor inválido")
        );
    }
}