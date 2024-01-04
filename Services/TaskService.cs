using HemFixBack.Models;
using HemFixBack.Repositories;
using System.Reflection;

namespace HemFixBack.Services
{
  public class TaskService : ITaskService
  {
    public readonly TaskFactory _taskFactory;
    public TaskService(TaskFactory taskFactory)
    {
      _taskFactory = taskFactory;
    }
    public ITask Create(string tableName, ITask task)
    {
      if (Database.CreateRecord(task) == false)
      {
        task = null;
      }
      return task;
    }

    public ITask Get(string tableName, string id)
    {
      object[] record = Database.ReadRecord(tableName, id);
      if (record == null)
      {
        return null;
      }

      ITask newTask = _taskFactory.CreateTaskInstance(tableName);
      PropertyInfo[] properties = newTask.GetType().GetProperties();
      properties = properties.OrderBy(p => p.Name).ToArray();
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

    public List<ITask> List(string tableName)
    {
      var records = Database.ListRecords(tableName);
      if (records is null)
      {
        return null;
      }

      List<ITask> tasks = new List<ITask>();
      foreach (var record in records)
      {
        ITask newTask = _taskFactory.CreateTaskInstance(tableName);
        PropertyInfo[] properties = newTask.GetType().GetProperties();
        properties = properties.OrderBy(p => p.Name).ToArray();
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
        tasks.Add(newTask);
      }
      return tasks;
    }

    public ITask Update(string tableName, ITask newTask)
    {
      if (Delete(tableName, newTask.Id))
      {
        Create(tableName, newTask);
      }
      else
      {
        return null;
      }
      return newTask;
    }

    public bool Delete(string tableName, string id)
    {
      return Database.DeleteRecord(tableName, id);
    }
  }
}
