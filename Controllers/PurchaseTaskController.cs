using HemFixBack.Models;
using HemFixBack.Services;

namespace HemFixBack.Controllers
{
  public class PurchaseTaskController
  {
    private readonly IPurchaseTaskService _service;

    public PurchaseTaskController(IPurchaseTaskService service)
    {
      _service = service;
    }

    public IResult Create(PurchaseTask task)
    {
      var result = _service.Create(task);
      if (task is null)
      {
        return Results.BadRequest("Task not saved");
      }
      return Results.Ok(result); //todo. CreatedAtAction med routing istället för Ok
    }

    public IResult Get(string id)
    {
      var task = _service.Get(id);
      if (task is null)
      {
        return Results.NotFound("Task not found");
      }
      return Results.Ok(task);
    }

    public IResult List()
    {
      var task = _service.List();
      if (task is null)
      {
        return Results.NotFound("Tasks not found");
      }
      return Results.Ok(task);
    }

    public IResult Update(PurchaseTask newTask)
    {
      var updatedTask = _service.Update(newTask);
      if (updatedTask is null)
      {
        Results.NotFound("Task not found");
      }
      return Results.Ok(updatedTask);
    }

    public IResult Delete(string id)
    {
      var result = _service.Delete(id);
      if (result == false)
      {
        Results.BadRequest("Something went wrong");
      }
      return Results.Ok(result); //todo. känns lite fel med true som result
    }
  }
}
