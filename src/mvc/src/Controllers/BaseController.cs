namespace TaskManager.MVC.Controllers;
public class BaseController : ControllerBase
{
    protected int UserId
    {
        get
        {
            var userId = Request.Headers["userId"].ToString();
            if (!string.IsNullOrEmpty(userId))
                return int.Parse(userId);
            throw new Exception("Usuario deve estar autenticado para essa solicitação.");
        }
    }
}
