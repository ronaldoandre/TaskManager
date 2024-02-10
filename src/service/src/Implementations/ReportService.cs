namespace TaskManager.Service.Implementations;
public class ReportService(IBaseRepository<TaskEntity> repoTask) : IReportService
{
    public async Task<IList<ReportEntity>> GetReport(int userId)
    {
        var tasks = await repoTask.Get(task => task.Status.Equals(TaskStatusEnum.Concluded) & task.LastUpdatedAt >= DateTime.Now.AddDays(-30));
        var userIds = tasks.Select(task => task.UserId).Distinct().ToList();
        return userIds.Select(userId => CreateReport(userId, tasks.Count(task => task.UserId.Equals(userId)))).ToList();
    }

    private ReportEntity CreateReport(int userId, int taskConcludedCount)
    {
        return new()
        {
            UserId = userId,
            TaskConcludedCount = taskConcludedCount,
        };
    }
}
