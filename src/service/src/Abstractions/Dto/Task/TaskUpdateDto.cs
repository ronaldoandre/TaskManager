namespace TaskManager.Service.Abstractions.Dto;

public class TaskUpdateDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public TaskStatusEnum Status { get; set; }
}
