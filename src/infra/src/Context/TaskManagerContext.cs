namespace TaskManager.Infra.Context;

class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    public DbSet<CommentEntity> Comments { get; set; }

    public DbSet<LogEntity> Logs { get; set; }
}
