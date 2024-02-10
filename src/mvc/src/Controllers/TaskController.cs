namespace TaskManager.MVC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Post(TaskInsertDto task)
    {
        try
        {
            return CreatedAtAction(nameof(Post), new ResponseDto((await taskService.CreateTask(task.MapperTo(), UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpGet("{projectId}")]
    public async Task<IActionResult> Get([FromRoute] int projectId)
    {
        try
        {
            return Ok(new ResponseDto((await taskService.GetTasks(projectId, UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] TaskUpdateDto task)
    {
        try
        {
            return Ok(new ResponseDto((await taskService.UpdateTask(task.MapperTo(), UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> Delete([FromRoute] int taskId)
    {
        try
        {
            await taskService.DeleteTask(taskId, UserId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpGet("{taskId}/Comments")]
    public async Task<IActionResult> GetComment([FromRoute] int taskId)
    {
        try
        {
            return Ok(new ResponseDto((await taskService.GetComments(taskId, UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpPost("{taskId}/Comment")]
    public async Task<IActionResult> PostComment([FromBody] CommentInsertDto comment, int taskId)
    {
        try
        {
            return CreatedAtAction(nameof(PostComment), new ResponseDto((await taskService.CreateComment(comment.MapperTo(), taskId, UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpPut("{taskId}/Comments")]
    public async Task<IActionResult> PutComment([FromBody] CommentUpdateDto comment, int taskId)
    {
        try
        {
            return Ok(new ResponseDto((await taskService.UpdateComment(comment.MapperTo(), taskId, UserId)).MapperTo()));
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }

    [HttpDelete("Comment/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        try
        {
            await taskService.DeleteComment(commentId, UserId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto(null, ex.Message));
        }
    }
}
