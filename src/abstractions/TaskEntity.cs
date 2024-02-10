namespace TaskManager.Abstractions;
public class TaskEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public TaskStatusEnum Status { get; set; }

    public PriorityEnum Priority { get; set; }

    public IList<CommentEntity> Comments { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public ProjectEntity Project { get; set; }

    public IList<LogEntity> Logs { get; set; }
}
