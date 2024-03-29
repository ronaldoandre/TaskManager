namespace TaskManager.MVC.Abstractions;

public class TaskResponseDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public TaskStatusEnum Status { get; set; }

    public PriorityEnum Priority { get; set; }

    public IList<CommentResponseDto>? Comments { get; set; }
}
