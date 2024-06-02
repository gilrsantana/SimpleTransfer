namespace SimpleTransfer.API.Notifications;

public class MessageModel
{
    public EMessageType Type { get; private set; }
    public string Text { get; private set; }
    
    public MessageModel(string text, EMessageType type = EMessageType.Information)
    {
        Type = type;
        Text = text;
    }
}