using HemFixBack.Models;
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

    public IResult Create(string tableName, ITask task)
    {
      var result = _service.Create(tableName, task);
      if (task is null)
      {
        return Results.BadRequest("Task not saved");
      }
      return Results.Ok(result); //todo. CreatedAtAction med routing istället för Ok
    }

    public IResult Get(string tableName, string id)
    {
      var task = _service.Get(tableName, id);
      if (task is null)
      {
        return Results.NotFound("Task not found");
      }
      return Results.Ok(task);
    }

    public IResult List(string tableName)
    {
      var task = _service.List(tableName);
      if (task is null)
      {
        return Results.NotFound("Tasks not found");
      }
      return Results.Ok(task);
    }

    public IResult Update(string tableName, ITask newTask)
    {
      var updatedTask = _service.Update(tableName, newTask);
      if (updatedTask is null)
      {
        Results.NotFound("Task not found");
      }
      return Results.Ok(updatedTask);
    }

    public IResult Delete(string tableName, string id)
    {
      var result = _service.Delete(tableName, id);
      if (result == false)
      {
        Results.BadRequest("Something went wrong");
      }
      return Results.Ok(result); //todo. känns lite fel med true som result
    }
  }
}
