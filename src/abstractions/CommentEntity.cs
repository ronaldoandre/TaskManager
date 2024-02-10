namespace TaskManager.Abstractions;
public class CommentEntity : BaseEntity
{
    public string Value { get; set; }

    public int UserId { get; set; }

    public int TaskId { get; set; }

    public TaskEntity Task { get; set; }
}
