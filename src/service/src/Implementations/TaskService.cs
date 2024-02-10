namespace TaskManager.Service.Implementations;
class TaskService(IBaseRepository<TaskEntity> repoTask, IBaseRepository<CommentEntity> repoComment) : BaseService, ITaskService
{
    public async Task<TaskResponseDto> CreateTask(TaskInsertDto task, int userId)
    {
        var tasks = await repoTask.Get(task => task.ProjectId == task.ProjectId & task.UserId == userId);
        if (tasks.Count >= 20)
            throw new Exception("Não é possivel ter mais de 20 tarefas em um projeto.");
        var taskMap = MapperTo(task);
        taskMap.UserId = userId;
        return MapperTo(await repoTask.Insert(taskMap));
    }

    public async Task DeleteComment(int commentId, int userId)
    {
        var comment = await repoComment.GetById(commentId);
        if (comment.UserId != userId)
            throw new Exception("Esta comentario não pertence a esse usuario.");
        await repoComment.Delete(comment);
    }

    public async Task DeleteTask(int taskId, int userId)
    {
        var task = await repoTask.GetById(taskId);
        if (task.UserId != userId)
            throw new Exception("Esta tarefa não pertence a esse usuario.");
        await repoTask.Delete(task);
    }

    public async Task<IList<CommentResponseDto>> GetComments(int taskId, int userId)
    {
        var comments = await repoComment.Get(comment => comment.TaskId == taskId & comment.UserId == userId);
        return comments.Select(MapperTo).ToList();
    }

    public async Task<IList<TaskResponseDto>> GetTasks(int projectId, int userId)
    {
        var tasks = await repoTask.Get(task => task.ProjectId == projectId & task.UserId == userId);
        return tasks.Select(MapperTo).ToList();
    }

    public async Task<CommentResponseDto> CreateComment(CommentInsertDto comment, int taskId, int userId)
    {
        var commentMap = MapperTo(comment);
        commentMap.UserId = userId;
        commentMap.TaskId = taskId;
        return MapperTo(await repoComment.Insert(commentMap));
    }

    public async Task<CommentResponseDto> UpdateComment(CommentUpdateDto comment, int userId)
    {
        var commentMap = await repoComment.GetById(comment.Id);
        commentMap.Value = comment.Value;
        return MapperTo(await repoComment.Update(commentMap));
    }

    public async Task<TaskResponseDto> UpdateTask(TaskUpdateDto task, int userId)
    {
        var taskDb = await repoTask.GetById(task.Id);
        taskDb.Title = task.Title;
        taskDb.Description = task.Description;
        taskDb.Status = task.Status;
        return MapperTo(await repoTask.Update(taskDb));
    }

    private TaskResponseDto MapperTo(TaskEntity task)
    {
        return new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
        };
    }

    private TaskEntity MapperTo(TaskInsertDto task)
    {
        return new()
        {
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
        };
    }

    private CommentEntity MapperTo(CommentInsertDto comment)
    {
        return new()
        {
            Value = comment.Value,
        };
    }

    private CommentResponseDto MapperTo(CommentEntity comment)
    {
        return new()
        {
            Id = comment.Id,
            Value = comment.Value,
        };
    }
}
