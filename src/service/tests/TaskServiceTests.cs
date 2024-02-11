namespace TaskManager.Service.Tests;

public class TaskServiceTests
{
    private readonly IBaseRepository<TaskEntity> taskRepository = new BaseRepositoryMock().ObjectTask;
    private readonly IBaseRepository<LogEntity> logRepository = new BaseRepositoryMock().ObjectLog;
    private readonly IBaseRepository<CommentEntity> commentRepository = new BaseRepositoryMock().ObjectComment;

    [Fact]
    public async Task Update_task_deve_alterar_description()
    {
        var taskService = new TaskService(taskRepository, commentRepository, logRepository);
        var taskUpdate = new TaskEntity()
        {
            Id = 1,
            Title = "Task Title",
            Description = "Task Update Description",
            UserId = 1,
            Status = TaskStatusEnum.Pending,
            Priority = 0,
        };
        var result = await taskService.UpdateTask(taskUpdate, 1);

        result.Description.Should().BeEquivalentTo(taskUpdate.Description);
    }

    [Fact]
    public async Task Create_task_deve_retornar_erro()
    {
        var taskService = new TaskService(taskRepository, commentRepository, logRepository);
        var taskInsert = new TaskEntity()
        {
            Id = 0,
            Title = "Task Title 21",
            Description = "Task Description",
            UserId = 1,
            Status = TaskStatusEnum.Pending,
            Priority = 0,
        };
        var result = () => taskService.CreateTask(taskInsert, 1);

        await result.Should().ThrowAsync<Exception>("Não é possivel ter mais de 20 tarefas em um projeto.");
    }
}
