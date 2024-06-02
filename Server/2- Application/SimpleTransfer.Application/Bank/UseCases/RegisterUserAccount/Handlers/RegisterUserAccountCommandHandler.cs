using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Commands;
using SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Notifications;
using SimpleTransfer.Application.Notifications;
using SimpleTransfer.Common.Interfaces;
using SimpleTransfer.Domain.BankAggregate.Entities;
using SimpleTransfer.Domain.BankAggregate.Interfaces;
using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Handlers;

public class RegisterUserAccountCommandHandler(
    UserManager<User> userManager, 
    IAccountBankRepository accountBankRepository,
    IUnitOfWork unitOfWork) 
    : IRequestHandler<RegisterUserAccountCommand, object>
{
    public async Task<object> Handle(RegisterUserAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user is null)
            return new ErrorNotification(new[] {"Usuário não encontrado"});

        var account = AccountBank.Create(user);
        if (!account.IsValid)
            return new ErrorNotification(account.Notifications
                .Select(x => x.Message).AsEnumerable());

        accountBankRepository.AddAsync(account);
        var result = await unitOfWork.SaveChangesAsync();
        if (result <= 0)
            return new ErrorNotification(new[] {"Erro ao salvar conta"});
        
        return new RegisteredUserAccountNotification(
            account.Id, user.Id, user.UserName ?? "", user.Document.Number);
    }
    
}