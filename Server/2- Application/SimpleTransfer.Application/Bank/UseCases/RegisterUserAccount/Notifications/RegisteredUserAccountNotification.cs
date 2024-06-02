using MediatR;

namespace SimpleTransfer.Application.Bank.UseCases.RegisterUserAccount.Notifications;

public record RegisteredUserAccountNotification(string Id, string UserId, string UserName, string DocumentNumber) : INotification;