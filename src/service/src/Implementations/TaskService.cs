namespace TaskManager.Service.Implementations;
class TaskService(
    IBaseRepository<TaskEntity> repoTask,
    IBaseRepository<CommentEntity> repoComment,
    IBaseRepository<LogEntity> repoLog) : ITaskService
{
    public async Task<TaskEntity> CreateTask(TaskEntity task, int userId)
    {
        var tasks = await repoTask.Get(task => task.ProjectId == task.ProjectId & task.UserId == userId);
        if (!tasks.Any() & tasks.Count >= 20)
            throw new Exception("Não é possivel ter mais de 20 tarefas em um projeto.");
        task.UserId = userId;
        return await repoTask.Insert(task);
    }

    public async Task DeleteComment(int commentId, int userId)
    {
        var comment = await repoComment.GetById(commentId);
        if (comment is null)
            throw new Exception("Este comentario não existe.");
        if (comment.UserId != userId)
            throw new Exception("Este comentario não pertence a esse usuario.");
        await repoComment.Delete(comment);
    }

    public async Task DeleteTask(int taskId, int userId)
    {
        var task = await repoTask.GetById(taskId);
        if (task is null)
            throw new Exception("Esta tarefa não existe.");
        if (task.UserId != userId)
            throw new Exception("Esta tarefa não pertence a esse usuario.");
        await repoTask.Delete(task);
    }

    public async Task<IList<CommentEntity>> GetComments(int taskId, int userId)
    {
        return await repoComment.Get(comment => comment.TaskId == taskId & comment.UserId == userId);
    }

    public async Task<IList<TaskEntity>> GetTasks(int projectId, int userId)
    {
        return await repoTask.Get(task => task.ProjectId == projectId & task.UserId == userId);
    }

    public async Task<CommentEntity> CreateComment(CommentEntity comment, int taskId, int userId)
    {
        comment.UserId = userId;
        comment.TaskId = taskId;
        var commentResult = await repoComment.Insert(comment);
        await RegisterLogs(CreateCommentLog(comment, taskId, userId));
        return commentResult;
    }

    public async Task<CommentEntity> UpdateComment(CommentEntity comment, int taskId, int userId)
    {
        var commentDb = await repoComment.GetById(comment.Id);
        var logs = CreateCommentLog(comment, taskId, userId, commentDb);
        commentDb.Value = comment.Value;
        var commentResult = await repoComment.Update(commentDb);
        await RegisterLogs(logs);
        return commentResult;
    }

    public async Task<TaskEntity> UpdateTask(TaskEntity task, int userId)
    {
        var taskDb = await repoTask.GetById(task.Id);
        var logs = CreateTaskLog(task, userId, taskDb);
        taskDb.Title = task.Title;
        taskDb.Description = task.Description;
        taskDb.Status = task.Status;
        var taskResult = await repoTask.Update(taskDb);
        await RegisterLogs(logs);
        return taskResult;
    }

    private IList<LogEntity> CreateCommentLog(CommentEntity newComment, int taskId, int userId, CommentEntity? oldComment = null)
    {
        return new List<LogEntity>()
        {
            new("Value", oldComment?.Value, newComment.Value, userId, taskId),
        };
    }

    private IList<LogEntity> CreateTaskLog(TaskEntity newTask, int userId, TaskEntity oldTask)
    {
        var taskId = newTask.Id;
        List<LogEntity> logEntities = new();
        if (newTask.Title != oldTask.Title)
            logEntities.Add(new("Title", oldTask.Title, newTask.Title, userId, taskId));

        if (newTask.Status != oldTask.Status)
            logEntities.Add(new("Status", oldTask.Status.ToString(), newTask.Status.ToString(), userId, taskId));

        if (newTask.Description != oldTask.Description)
            logEntities.Add(new("Description", oldTask.Description, newTask.Description, userId, taskId));

        return logEntities;
    }

    private async Task RegisterLogs(IList<LogEntity> logs)
    {
        await repoLog.InsertRange(logs);
    }
}
