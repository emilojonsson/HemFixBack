using HemFixBack.Models;

namespace HemFixBack
{
  public class TaskFactory
  {
    public ITask CreateTaskInstance(string tableName)
    {
      switch (tableName)
      {
        case "simpletask":
          return new SimpleTask();
        case "gardentask":
          return new GardenTask();
        case "maintenancetask":
          return new MaintenanceTask();
        case "purchasetask":
          return new PurchaseTask();
        default:
          throw new InvalidOperationException("Unknown task type");
      }
    }
  }
}