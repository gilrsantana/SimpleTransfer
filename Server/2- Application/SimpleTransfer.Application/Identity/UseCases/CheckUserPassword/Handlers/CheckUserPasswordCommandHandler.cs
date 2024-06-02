using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MediatR;
using SimpleTransfer.Application.Identity.Token.Interface;
using SimpleTransfer.Application.Identity.UseCases.CheckUserPassword.Commands;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Notifications;
using SimpleTransfer.Application.Notifications;
using SimpleTransfer.Domain.IdentityAggregate.Entities;
using SimpleTransfer.Domain.IdentityAggregate.Interfaces;

namespace SimpleTransfer.Application.Identity.UseCases.CheckUserPassword.Handlers;

public class CheckUserPasswordCommandHandler(IUserRepository userRepository, ITokenService tokenService) 
    : IRequestHandler<CheckUserPasswordCommand, object>
{
    public async Task<object> Handle(CheckUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUser(request);
        if (user is null)
            return new ErrorNotification(["Usuário não encontrado"]);
        
        if (!user.CheckPassword(request.Password))
            return new ErrorNotification(["Senha inválida"]);
        
        return new RegisteredUserNotification
            (user.Id, user.Document.Number, user.Email ?? "", user.UserName ?? "", tokenService.GenerateToken(user));
    }
    
    private async Task<User?> GetUser(CheckUserPasswordCommand request)
    {
        if (!string.IsNullOrWhiteSpace(request.UserName))
            return await GetUserByUserName(request.UserName);
        if (!string.IsNullOrWhiteSpace(request.DocumentNumber))
            return await GetUserByDocumentNumber(request.DocumentNumber);
        if (!string.IsNullOrWhiteSpace(request.Email))
            return await GetUserByEmail(request.Email);
        return null;
    }
    
    private async Task<User?> GetUserByUserName(string userName)
    {
        var name = userName.Trim().ToUpper();
        Expression<Func<User, bool>> filter = user => user.UserName == userName;
        var user = await userRepository.GetByFilterAsync(filter);
        return user.Any()
            ? user[0]
            : null;
    }
    
    private async Task<User?> GetUserByDocumentNumber(string documentNumber)
    {
        var number = Regex.Replace(documentNumber, @"\D", "");
        Expression<Func<User, bool>> filter = user => user.Document.Number == number;
        var user = await userRepository.GetByFilterAsync(filter);
        return user.Any()
            ? user[0]
            : null;
    }
    
    private async Task<User?> GetUserByEmail(string email)
    {
        var mail = email.Trim().ToLower();
        Expression<Func<User, bool>> filter = user => user.Email == mail;
        var user = await userRepository.GetByFilterAsync(filter);
        return user.Any()
            ? user[0]
            : null;
    }
}
