namespace TaskManager.MVC.Abstractions;
public class ProjectResponseDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public IList<TaskResponseDto>? Tasks { get; set; }
}
