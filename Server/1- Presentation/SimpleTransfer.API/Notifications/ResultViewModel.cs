namespace SimpleTransfer.API.Notifications;

public class ResultViewModel<T>
{
    public T? ViewData { get; private set; }
    public List<MessageModel> Messages { get; private set; } = new();

    public ResultViewModel(T viewData)
    {
        ViewData = viewData;
    }

    public ResultViewModel(List<MessageModel> messages)
    {
        Messages = messages;
    }
    
    public ResultViewModel(T? viewData, List<MessageModel> messages)
    {
        ViewData = viewData;
        Messages = messages;
    }
    
    public ResultViewModel(MessageModel message)
    {
        Messages.Add(message);
    }

    public ResultViewModel(string message, EMessageType type = EMessageType.Information)
    {
        Messages.Add(new MessageModel(message, type));
    }

    public ResultViewModel(IReadOnlyCollection<string> list, EMessageType type = EMessageType.Information)
    {
        Messages = !list.Any()
            ? new List<MessageModel>()
            : list.Select(message => new MessageModel(message, type)).ToList();  
    }
}