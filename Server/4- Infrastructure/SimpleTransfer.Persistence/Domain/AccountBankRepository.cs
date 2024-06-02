using SimpleTransfer.Data.Context;
using SimpleTransfer.Domain.BankAggregate.Entities;
using SimpleTransfer.Domain.BankAggregate.Interfaces;
using SimpleTransfer.Persistence.Common;

namespace SimpleTransfer.Persistence.Domain;

public class AccountBankRepository(SimpleTransferContext context) 
    : Repository<AccountBank>(context), IAccountBankRepository;