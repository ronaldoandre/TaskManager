namespace TaskManager.Service.Implementations;
class ProjectService(IBaseRepository<ProjectEntity> repoProject) : BaseService, IProjectService
{
    public async Task<ProjectResponseDto> CreateProject(ProjectInsertDto project, int userId)
    {
        var projectMap = MapperTo(project);
        projectMap.UserId = userId;
        var projectDb = await repoProject.Insert(projectMap);
        return MapperTo(projectDb);
    }

    public async Task<bool> DeleteProject(int projectId, int userId)
    {
        var project = await repoProject.GetById(projectId, project => project.Tasks);

        if (project.UserId != userId)
            throw new Exception("Este projeto não pertence a este usuario.");

        if (project.Tasks.FirstOrDefault(x =>
            x.Status.Equals(
                TaskStatusEnum.Concluded |
                TaskStatusEnum.Canceled)) is not null)
            throw new Exception("Não é possivel remover projetos com tarefas pendentes, conclua ou cancele todas as tarefas pendentes.");

        return await repoProject.Delete(project);
    }

    public async Task<IList<ProjectResponseDto>> GetProjects(int userId)
    {
        var projects = await repoProject.Get(project => project.UserId == userId);
        return projects.Select(MapperTo).ToList();
    }

    private ProjectResponseDto MapperTo(ProjectEntity project)
    {
        return new()
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
        };
    }

    private ProjectEntity MapperTo(ProjectInsertDto project)
    {
        return new()
        {
            Title = project.Title,
            Description = project.Description,
        };
    }
}
