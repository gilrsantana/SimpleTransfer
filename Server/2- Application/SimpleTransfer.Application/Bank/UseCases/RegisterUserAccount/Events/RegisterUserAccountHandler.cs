using MediatR;
using SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Commands;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Notifications;
using SimpleTransfer.Application.Notifications;

namespace SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Events;

public class RegisterUserAccountHandler(IMediator mediator) : INotificationHandler<RegisteredUserNotification>
{
    public async Task Handle(RegisteredUserNotification notification, CancellationToken cancellationToken)
    {
        var command = new RegisterUserAccountCommand(notification.Id);
        var result = await mediator.Send(command, cancellationToken);
        if (result is ErrorNotification error)
            await mediator.Publish(error, cancellationToken);
        else
            await mediator.Publish(result, cancellationToken);
    }
}