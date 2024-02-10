using TaskManager.Service.Abstractions;
using TaskManager.Service.Implementations;

namespace TaskManager.Extensions;
static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        return services;
    }
}
