namespace TaskManager.MVC.Abstractions;

public class TaskInsertDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public PriorityEnum Priority { get; set; }

    public int ProjectId { get; set; }
}
