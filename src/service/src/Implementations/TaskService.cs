namespace TaskManager.Service.Implementations;
class TaskService(IBaseRepository<TaskEntity> repoTask, IBaseRepository<CommentEntity> repoComment) : BaseService, ITaskService
{
    public async Task<TaskEntity> CreateTask(TaskEntity task, int userId)
    {
        var tasks = await repoTask.Get(task => task.ProjectId == task.ProjectId & task.UserId == userId);
        if (tasks.Count >= 20)
            throw new Exception("Não é possivel ter mais de 20 tarefas em um projeto.");
        task.UserId = userId;
        return await repoTask.Insert(task);
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
        return await repoComment.Insert(comment);
    }

    public async Task<CommentEntity> UpdateComment(CommentEntity comment, int userId)
    {
        var commentMap = await repoComment.GetById(comment.Id);
        commentMap.Value = comment.Value;
        return await repoComment.Update(commentMap);
    }

    public async Task<TaskEntity> UpdateTask(TaskEntity task, int userId)
    {
        var taskDb = await repoTask.GetById(task.Id);
        taskDb.Title = task.Title;
        taskDb.Description = task.Description;
        taskDb.Status = task.Status;
        return await repoTask.Update(taskDb);
    }
}
