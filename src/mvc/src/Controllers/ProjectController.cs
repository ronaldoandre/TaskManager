namespace TaskManager.MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProjectInsertDto project)
    {
        try
        {
            return Ok(new ResponseDto((await projectService.CreateProject(project.MapperTo(), UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            return Ok(new ResponseDto((await projectService.GetProjects(UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpDelete("{projectId}")]
    public async Task<IActionResult> Delete([FromRoute] int projectId)
    {
        try
        {
            return Ok(new ResponseDto(await projectService.DeleteProject(projectId, UserId)));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }
}
