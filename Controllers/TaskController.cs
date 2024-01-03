using HemFixBack.Services;

namespace HemFixBack.Controllers
{
  public class TaskController
  {
    private readonly ITaskService _service;

    public TaskController(ITaskService service)
    {
      _service = service;
    }

    public IResult List()
    {
      var tasks = _service.List();
      if (tasks is null)
      {
        return Results.NotFound("Tasks not found");
      }
      return Results.Ok(tasks);
    }
  }
}
