using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using TaskManager.Infra.Context;

namespace TaskManager.Extensions;
static class MySqlExtensions
{
    public static IServiceCollection AddMySql(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TaskManager");
        services.AddDbContext<TaskManagerContext>(options =>
            options.UseMySql(connectionString, ServerVersion.Create(new Version("8.0"), ServerType.MySql), b => b.MigrationsAssembly("TaskManager").EnableRetryOnFailure()));
        return services;
    }

    public static IServiceProvider ExecuteMigrations(this IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<TaskManagerContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        return service;
    }
}
