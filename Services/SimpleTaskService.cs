using System.Reflection;
using HemFixBack.Models;
using HemFixBack.Repositories;

namespace HemFixBack.Services
{
  public class SimpleTaskService : ISimpleTaskService
  {
    public SimpleTask Create(SimpleTask task)
    {
      if (Database.CreateRecord(task) == false)
      {
        task = null;
      }
      return task;
    }

    public SimpleTask Get(string id)
    {
      string tableName = "simpletask";
      object[] record = Database.ReadRecord(tableName, id);
      if (record == null)
      {
        return null;
      }

      SimpleTask newTask = new SimpleTask();
      PropertyInfo[] properties = typeof(SimpleTask).GetProperties();
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

    public List<SimpleTask> List()
    {
      string tableName = "simpletask";
      var records = Database.ListRecords(tableName);
      if (records is null)
      {
        return null;
      }

      List<SimpleTask> simpleTasks = new List<SimpleTask>();
      foreach (var record in records)
      {
        SimpleTask newTask = new SimpleTask();
        PropertyInfo[] properties = typeof(SimpleTask).GetProperties();
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
        simpleTasks.Add(newTask);
      }
      return simpleTasks;
    }

    public SimpleTask Update(SimpleTask newTask)
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
      string tableName = "simpletask";
      return Database.DeleteRecord(tableName, id);
    }
  }
}
