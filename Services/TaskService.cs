using HemFixBack.Models;
using Serilog;

namespace HemFixBack.Services
{
  public class TaskService : ITaskService
  {
    private readonly IGardenTaskService _gardenTaskService;
    private readonly IMaintenanceTaskService _maintenanceTaskService;

    public TaskService(
      IGardenTaskService gardenTaskService,
      IMaintenanceTaskService maintenanceTaskService
    )
    {
      _gardenTaskService = gardenTaskService;
      _maintenanceTaskService = maintenanceTaskService;
    }

    public List<ITask> List()
    {
      var gardenTasks = _gardenTaskService.List();
      var maintenanceTasks = _maintenanceTaskService.List();

      var allTasks = new List<ITask>();
      allTasks.AddRange(gardenTasks);
      allTasks.AddRange(maintenanceTasks);

      Log.Information("Serialized tasks: {@AllTasks}", allTasks);

      return allTasks;
    }
  }
}
