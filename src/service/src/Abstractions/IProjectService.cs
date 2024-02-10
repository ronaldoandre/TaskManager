namespace TaskManager.Service.Abstractions;

public interface IProjectService
{
    Task<IList<ProjectResponseDto>> GetProjects(int userId);

    Task<ProjectResponseDto> CreateProject(ProjectInsertDto project, int userId);

    Task<bool> DeleteProject(int projectId, int userId);
}
