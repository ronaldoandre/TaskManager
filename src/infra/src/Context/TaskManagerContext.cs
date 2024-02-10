namespace TaskManager.Infra.Context;

class TaskManagerContext(DbContextOptions<TaskManagerContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    public DbSet<CommentEntity> Comments { get; set; }

    public DbSet<LogEntity> Logs { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var createEntity = ChangeTracker.Entries().Where(entity => entity.State == EntityState.Added).ToList();

            createEntity.ForEach(entity =>
            {
                entity.Property("CreatedAt").CurrentValue = DateTime.Now;
            });

            var updateEntity = ChangeTracker.Entries().Where(entity => entity.State == EntityState.Modified).ToList();

            updateEntity.ForEach(entity =>
            {
                entity.Property("LastUpdatedAt").CurrentValue = DateTime.Now;
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
}
