namespace TaskManager.Abstractions;
public class LogEntity : BaseEntity
{
    public LogEntity(string property, string oldValue, string newValue, int userId, int taskId)
    {
        Property = property;
        OldValue = oldValue;
        NewValue = newValue;
        UserId = userId;
        TaskId = taskId;
    }

    public string Property { get; set; }

    public string OldValue { get; set; }

    public string NewValue { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }
}
