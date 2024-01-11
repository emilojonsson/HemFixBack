using HemFixBack.Interfaces;
using HemFixBack.Repositories;
using Npgsql;
using System.Collections;

namespace HemFixBack.Services
{
  public class TaskService : ITaskService
  {
    public readonly TaskFactory _taskFactory;
    public readonly Database _database;
    public TaskService(TaskFactory taskFactory, Database database)
    {
      _taskFactory = taskFactory;
      _database = database;
    }

    public ITask Create(string tableName, ITask task)
    {
      if (_database.CreateRecord(tableName, task) == false)
      {
        task = null;
      }
      return task;
    }

    public ITask Get(string tableName, string id)
    {
      using (var dataReader = _database.ReadRecord(tableName, id))
      {
        if (dataReader != null && dataReader.HasRows)
        {
          dataReader.Read();
          return ConvertDataReaderToTask(dataReader, tableName);
        }
      }
      return null;
    }

    public ArrayList List(string tableName)
    {
      ArrayList tasks = new ArrayList();
      using (var dataReader = _database.ListRecords(tableName))
      {
        if (dataReader != null && dataReader.HasRows)
        {
          while (dataReader.Read())
          {
            ITask newTask = ConvertDataReaderToTask(dataReader, tableName);
            tasks.Add(newTask);
          }
        }
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
      return _database.DeleteRecord(tableName, id);
    }

    private ITask ConvertDataReaderToTask(NpgsqlDataReader dataReader, string tableName)
    {
      ITask newTask = _taskFactory.CreateTaskInstance(tableName);
      newTask.SetAdditionalProperties(dataReader);
      return newTask;
    }
  }
}
