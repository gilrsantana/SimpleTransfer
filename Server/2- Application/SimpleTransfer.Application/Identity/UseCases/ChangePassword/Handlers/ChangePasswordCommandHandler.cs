using MediatR;
using Microsoft.AspNetCore.Identity;
using SimpleTransfer.Application.Identity.UseCases.ChangePassword.Commands;
using SimpleTransfer.Application.Notifications;
using SimpleTransfer.Domain.IdentityAggregate.Entities;

namespace SimpleTransfer.Application.Identity.UseCases.ChangePassword.Handlers;

public class ChangePasswordCommandHandler(UserManager<User> userManager) : IRequestHandler<ChangePasswordCommand, object>
{
    public async Task<object> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = userManager.FindByIdAsync(request.Id).Result;
        if (user is null)
            return new ErrorNotification(["Usuário não encontrado"]);
        
        user.ChangePassword(request.OldPassword, request.NewPassword);
        if (!user.IsValid)
            return new ErrorNotification(user.Notifications.Select(n => n.Message).ToList());

        var result = await userManager.UpdateAsync(user);
        
        if (result != IdentityResult.Success)
            return new ErrorNotification(result.Errors.Select(e => e.Description).ToList());
        
        return true;
    }
}