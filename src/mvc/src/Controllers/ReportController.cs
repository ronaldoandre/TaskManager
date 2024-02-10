namespace TaskManager.MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController(IReportService reportService) : BaseController
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        try
        {
            return Ok(new ResponseDto(await reportService.GetReport(UserId)));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }
}
