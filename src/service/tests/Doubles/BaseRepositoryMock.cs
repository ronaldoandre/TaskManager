namespace TaskManager.Service.Tests.Doubles;

public class BaseRepositoryMock
{
    public BaseRepositoryMock()
    {
        SetupUpdateTask();
        SetupGetByIdTask();
        SetupInsertLogs();
        SetupGetTask();
    }

    private readonly Mock<IBaseRepository<TaskEntity>> mockTask = new();

    private readonly Mock<IBaseRepository<LogEntity>> mockLog = new();

    private readonly Mock<IBaseRepository<CommentEntity>> mockComment = new();

    public IBaseRepository<TaskEntity> ObjectTask => mockTask.Object;

    public IBaseRepository<LogEntity> ObjectLog => mockLog.Object;

    public IBaseRepository<CommentEntity> ObjectComment => mockComment.Object;

    public BaseRepositoryMock SetupUpdateTask()
    {
        mockTask.Setup(o => o.Update(It.IsAny<TaskEntity>()))
            .ReturnsAsync(new TaskEntity()
            {
                Id = 1,
                Title = "Task Title",
                Description = "Task Update Description",
                UserId = 1,
                Status = TaskStatusEnum.Pending,
                Priority = 0,
            });
        return this;
    }

    public BaseRepositoryMock SetupGetByIdTask()
    {
        mockTask.Setup(o => o.GetById(It.IsAny<int>()))
            .ReturnsAsync(new TaskEntity()
            {
                Id = 1,
                Title = "Task Title",
                Description = "Task Description",
                UserId = 1,
                Status = TaskStatusEnum.Doing,
                Priority = 0,
            });
        return this;
    }

    public BaseRepositoryMock SetupGetTask()
    {
        mockTask.Setup(o => o.Get(It.IsAny<Expression<Func<TaskEntity, bool>>>()))
            .ReturnsAsync(new List<TaskEntity>()
            {
                new() { Id = 1, Title = "Task Title 1", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 2, Title = "Task Title 2", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 3, Title = "Task Title 3", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 4, Title = "Task Title 4", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 5, Title = "Task Title 5", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 6, Title = "Task Title 6", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 7, Title = "Task Title 7", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 8, Title = "Task Title 8", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 9, Title = "Task Title 9", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 10, Title = "Task Title 10", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 11, Title = "Task Title 11", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 12, Title = "Task Title 12", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 13, Title = "Task Title 13", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 14, Title = "Task Title 14", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 15, Title = "Task Title 15", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 16, Title = "Task Title 16", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 17, Title = "Task Title 17", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 18, Title = "Task Title 18", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 19, Title = "Task Title 19", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
                new() { Id = 20, Title = "Task Title 20", Description = "Task Description", UserId = 1, Status = TaskStatusEnum.Pending, Priority = 0, },
            });
        return this;
    }

    public BaseRepositoryMock SetupInsertLogs()
    {
        mockLog.Setup(o => o.InsertRange(It.IsAny<IList<LogEntity>>()));
        return this;
    }
}
