namespace TaskManager.Service.Abstractions;
public interface IReportService
{
    Task<IList<ReportEntity>> GetReport(int userId);
}
