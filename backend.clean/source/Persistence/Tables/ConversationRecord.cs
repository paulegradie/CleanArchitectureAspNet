using System.ComponentModel.DataAnnotations;
using Persistence.EntityFramework;

namespace Persistence.Tables;

[DatabaseModel]
public class ConversationRecord
{
#pragma warning disable CS8618
    public ConversationRecord()
#pragma warning restore CS8618
    {
    }

    public ConversationRecord(string[] conversationItems)
    {
        ConversationItems = conversationItems;
    }

    [Key]
    public int ConversationId { get; set; }
    public string[] ConversationItems { get; set; }
}