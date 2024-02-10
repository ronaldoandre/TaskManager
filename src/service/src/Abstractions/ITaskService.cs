namespace TaskManager.Service.Abstractions;

public interface ITaskService
{
    Task<IList<TaskEntity>> GetTasks(int projectId, int userId);

    Task<TaskEntity> CreateTask(TaskEntity task, int userId);

    Task<TaskEntity> UpdateTask(TaskEntity task, int userId);

    Task DeleteTask(int taskId, int userId);

    Task<IList<CommentEntity>> GetComments(int taskId, int userId);

    Task<CommentEntity> CreateComment(CommentEntity comment, int taskId, int userId);

    Task<CommentEntity> UpdateComment(CommentEntity comment, int taskId, int userId);

    Task DeleteComment(int commentId, int userId);
}
