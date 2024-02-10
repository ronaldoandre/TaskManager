namespace TaskManager.Abstractions;
public class LogEntity : BaseEntity
{
    public string Property { get; set; }

    public string Description { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }
}
