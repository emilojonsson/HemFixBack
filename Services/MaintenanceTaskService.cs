using System.Reflection;
using HemFixBack.Models;
using HemFixBack.Repositories;

namespace HemFixBack.Services
{
  public class MaintenanceTaskService : IMaintenanceTaskService
  {
    public MaintenanceTask Create(MaintenanceTask task)
    {
      if (Database.CreateRecord(task) == false)
      {
        task = null;
      }
      return task;
    }

    public MaintenanceTask Get(string id)
    {
      string tableName = "maintenancetask";
      object[] record = Database.ReadRecord(tableName, id);
      if (record == null)
      {
        return null;
      }

      MaintenanceTask newTask = new MaintenanceTask();
      PropertyInfo[] properties = typeof(MaintenanceTask).GetProperties();
      for (int i = 0; i < properties.Length; i++)
      {
        if (i < record.Length)
        {
          // Konvertera värdena om det behövs (till exempel från databastypen)
          object value = Convert.ChangeType(record[i], properties[i].PropertyType);
          // Sätt egenskapens värde
          properties[i].SetValue(newTask, value);
        }
      }
      return newTask;
    }

    public List<MaintenanceTask> List()
    {
      string tableName = "maintenancetask";
      var records = Database.ListRecords(tableName);
      if (records is null)
      {
        return null;
      }

      List<MaintenanceTask> maintenanceTasks = new List<MaintenanceTask>();
      foreach (var record in records)
      {
        MaintenanceTask newTask = new MaintenanceTask();
        PropertyInfo[] properties = typeof(MaintenanceTask).GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
          if (i < record.Length)
          {
            // Konvertera värdena om det behövs (till exempel från databastypen)
            object value = Convert.ChangeType(record[i], properties[i].PropertyType);
            // Sätt egenskapens värde
            properties[i].SetValue(newTask, value);
          }
        }
        maintenanceTasks.Add(newTask);
      }
      return maintenanceTasks;
    }

    public MaintenanceTask Update(MaintenanceTask newTask)
    {
      if (Delete(newTask.Id))
      {
        Create(newTask);
      }
      else
      {
        return null;
      }
      return newTask;
    }

    public bool Delete(string id)
    {
      string tableName = "maintenancetask";
      return Database.DeleteRecord(tableName, id);
    }
  }
}
