using SimpleTransfer.Data.Context;
using SimpleTransfer.Domain.BankTransactionsAggregate.Entities;
using SimpleTransfer.Domain.BankTransactionsAggregate.Interfaces;
using SimpleTransfer.Persistence.Common;

namespace SimpleTransfer.Persistence.Domain;

public class AccountBankRepository(SimpleTransferContext context) 
    : Repository<AccountBank>(context), IAccountBankRepository;