using System.Reflection;
using HemFixBack.Models;
using HemFixBack.Repositories;

namespace HemFixBack.Services
{
  public class PurchaseTaskService : IPurchaseTaskService
  {
    public PurchaseTask Create(PurchaseTask task)
    {
      if (Database.CreateRecord(task) == false)
      {
        task = null;
      }
      return task;
    }

    public PurchaseTask Get(string id)
    {
      string tableName = "purchasetask";
      object[] record = Database.ReadRecord(tableName, id);
      if (record == null)
      {
        return null;
      }

      PurchaseTask newTask = new PurchaseTask();
      PropertyInfo[] properties = typeof(PurchaseTask).GetProperties();
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

    public List<PurchaseTask> List()
    {
      string tableName = "purchasetask";
      var records = Database.ListRecords(tableName);
      if (records is null)
      {
        return null;
      }

      List<PurchaseTask> purchaseTasks = new List<PurchaseTask>();
      foreach (var record in records)
      {
        PurchaseTask newTask = new PurchaseTask();
        PropertyInfo[] properties = typeof(PurchaseTask).GetProperties();
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
        purchaseTasks.Add(newTask);
      }
      return purchaseTasks;
    }

    public PurchaseTask Update(PurchaseTask newTask)
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
      string tableName = "purchasetask";
      return Database.DeleteRecord(tableName, id);
    }
  }
}
