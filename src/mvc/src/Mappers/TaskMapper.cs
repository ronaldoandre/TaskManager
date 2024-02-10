namespace TaskManager.MVC.Mappers;
static class TaskMapper
{
    public static IList<TaskResponseDto> MapperTo(this IList<TaskEntity> tasks)
    {
        return tasks.Select(task => task.MapperTo()).ToList();
    }

    public static TaskResponseDto MapperTo(this TaskEntity task)
    {
        return new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            Comments = task.Comments?.MapperTo() ?? null,
        };
    }

    public static TaskEntity MapperTo(this TaskInsertDto task)
    {
        return new()
        {
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            ProjectId = task.ProjectId,
        };
    }

    public static TaskEntity MapperTo(this TaskUpdateDto task)
    {
        return new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
        };
    }
}
