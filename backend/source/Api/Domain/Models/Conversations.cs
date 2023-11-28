namespace Api.Domain.Models;

[Entity]
public class Conversation
{
#pragma warning disable CS8618
    public Conversation()
#pragma warning restore CS8618
    {
        
    }
    
    
    public Conversation(string[] conversationItems)
    {
        ConversationItems = conversationItems;
    }

    public int ConversationId { get; set; }
    public string[] ConversationItems { get; set; }
}