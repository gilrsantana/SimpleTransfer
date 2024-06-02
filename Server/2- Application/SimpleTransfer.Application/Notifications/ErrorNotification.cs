using MediatR;

namespace SimpleTransfer.Application.Notifications;

public record ErrorNotification(IEnumerable<string> Messages) : INotification;
