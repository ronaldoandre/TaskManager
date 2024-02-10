namespace TaskManager.Service.Abstractions.Dto;

public class TaskInsertDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public PriorityEnum Priority { get; set; }
}
