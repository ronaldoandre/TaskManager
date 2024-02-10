namespace TaskManager.Abstractions;
public class ProjectEntity : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public int UserId { get; set; }

    public IList<TaskEntity> Tasks { get; set; }
}
