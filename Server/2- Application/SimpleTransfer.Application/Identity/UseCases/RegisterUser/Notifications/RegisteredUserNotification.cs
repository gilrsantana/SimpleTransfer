using MediatR;
using SimpleTransfer.Domain.IdentityAggregate.ValueObjects.Factory;

namespace SimpleTransfer.Application.Identity.UseCases.RegisterUser.Notifications;

public record RegisteredUserNotification
    (string Id, string Document, string Email, string Username, string Token)
    : INotification;