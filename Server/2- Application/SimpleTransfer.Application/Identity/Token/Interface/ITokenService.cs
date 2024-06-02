using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Application.Identity.Token.Interface;

public interface ITokenService
{
    string GenerateToken(User user);
}