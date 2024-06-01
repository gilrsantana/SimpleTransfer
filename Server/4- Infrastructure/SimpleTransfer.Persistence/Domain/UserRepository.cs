using SimpleTransfer.Data.Context;
using SimpleTransfer.Domain.IdentityAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.Interfaces;
using SimpleTransfer.Persistence.Common;

namespace SimpleTransfer.Persistence.Domain;

public class UserRepository(SimpleTransferContext context) 
    : Repository<User>(context), IUserRepository;