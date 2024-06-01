using Flunt.Notifications;

namespace SimpleTransfer.Common.ValueObjects;

public abstract class BaseValueObject : Notifiable<Notification>
{
    public abstract void Validate();
}