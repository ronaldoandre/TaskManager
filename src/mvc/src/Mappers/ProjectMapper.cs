namespace TaskManager.MVC.Mappers;
static class ProjectMapper
{
    public static IList<ProjectResponseDto> MapperTo(this IList<ProjectEntity> projects)
    {
        return projects.Select(project => project.MapperTo()).ToList();
    }

    public static ProjectEntity MapperTo(this ProjectInsertDto project)
    {
        return new()
        {
            Title = project.Title,
            Description = project.Description,
        };
    }

    public static ProjectResponseDto MapperTo(this ProjectEntity project)
    {
        return new()
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Tasks = project.Tasks?.MapperTo() ?? null,
        };
    }
}
