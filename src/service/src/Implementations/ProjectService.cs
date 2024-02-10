namespace TaskManager.Service.Implementations;
class ProjectService(IBaseRepository<ProjectEntity> repoProject) : IProjectService
{
    public async Task<ProjectEntity> CreateProject(ProjectEntity project, int userId)
    {
        project.UserId = userId;
        return await repoProject.Insert(project);
    }

    public async Task<bool> DeleteProject(int projectId, int userId)
    {
        var project = await repoProject.GetById(projectId, project => project.Tasks);

        if (project is null)
            throw new Exception("Este projeto não existe.");

        if (project.UserId != userId)
            throw new Exception("Este projeto não pertence a este usuario.");

        if (project.Tasks.FirstOrDefault(x => x.Status == TaskStatusEnum.Pending | x.Status == TaskStatusEnum.Doing) != null)
            throw new Exception("Não é possivel remover projetos com tarefas pendentes, conclua ou cancele todas as tarefas.");

        return await repoProject.Delete(project);
    }

    public async Task<IList<ProjectEntity>> GetProjects(int userId)
    {
        return await repoProject.Get(project => project.UserId == userId);
    }
}
