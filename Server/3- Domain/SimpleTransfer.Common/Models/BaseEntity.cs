using Flunt.Notifications;

namespace SimpleTransfer.Common.Models;

public abstract class BaseEntity : Notifiable<Notification>
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    
    public abstract void Validate();
}