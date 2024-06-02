using MediatR;
using SimpleTransfer.Application.Identity.UseCases.UpdateUser.Commands;
using SimpleTransfer.Application.Identity.UseCases.UpdateUser.Notifications;
using SimpleTransfer.Application.Notifications;
using SimpleTransfer.Common.Interfaces;
using SimpleTransfer.Domain.IdentityAggregate.Interfaces;

namespace SimpleTransfer.Application.Identity.UseCases.UpdateUser.Handlers;

public class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand, object>
{
    public async Task<object> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user is null)
            return new ErrorNotification(["Usuário não encontrado"]);
        
        user.Update(request.Name, request.Email);
        if (!user.IsValid)
            return new ErrorNotification(user.Notifications.Select(n => n.Message).ToList());
        
        userRepository.UpdateAsync(user);
        return await unitOfWork.SaveChangesAsync() <= 0
            ? new ErrorNotification(["Erro ao atualizar usuário"])
            : new UpdatedUserNotification(user.Id, user.Document.Number, user.Email ?? "", user.UserName ?? "");
    }
}