namespace TaskManager.Service.Abstractions;

public interface IProjectService
{
    Task<IList<ProjectEntity>> GetProjects(int userId);

    Task<ProjectEntity> CreateProject(ProjectEntity project, int userId);

    Task<bool> DeleteProject(int projectId, int userId);
}
