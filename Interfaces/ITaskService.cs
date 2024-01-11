using System.Collections;

namespace HemFixBack.Interfaces
{
  public interface ITaskService
  {
    public ITask Create(string tableName, ITask task);
    public ITask Get(string tableName, string id);
    public ArrayList List(string tableName);
    public ITask Update(string tableName, ITask task);
    public bool Delete(string tableName, string id);
  }
}
