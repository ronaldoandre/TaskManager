using TaskManager.Abstractions;
using TaskManager.Infra.Abstractions.Repository;
using TaskManager.Infra.Repository;

namespace TaskManager.Extensions;
static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<ProjectEntity>, BaseRepository<ProjectEntity>>();
        services.AddScoped<IBaseRepository<TaskEntity>, BaseRepository<TaskEntity>>();
        services.AddScoped<IBaseRepository<CommentEntity>, BaseRepository<CommentEntity>>();
        services.AddScoped<IBaseRepository<LogEntity>, BaseRepository<LogEntity>>();
        return services;
    }
}
