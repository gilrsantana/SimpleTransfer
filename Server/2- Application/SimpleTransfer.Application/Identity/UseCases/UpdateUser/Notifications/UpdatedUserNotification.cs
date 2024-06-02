using MediatR;
using SimpleTransfer.Application.Identity.UseCases.RegisterUser.Notifications;

namespace SimpleTransfer.Application.Identity.UseCases.UpdateUser.Notifications;

public record UpdatedUserNotification(string Id, string Document, string Email, string Username)
    : INotification;