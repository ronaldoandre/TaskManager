namespace TaskManager.Service.Abstractions;

public interface ITaskService
{
    Task<IList<TaskResponseDto>> GetTasks(int projectId, int userId);

    Task<TaskResponseDto> CreateTask(TaskInsertDto task, int userId);

    Task<TaskResponseDto> UpdateTask(TaskUpdateDto task, int userId);

    Task DeleteTask(int taskId, int userId);

    Task<IList<CommentResponseDto>> GetComments(int taskId, int userId);

    Task<CommentResponseDto> CreateComment(CommentInsertDto comment, int taskId, int userId);

    Task<CommentResponseDto> UpdateComment(CommentUpdateDto comment, int userId);

    Task DeleteComment(int commentId, int userId);
}
