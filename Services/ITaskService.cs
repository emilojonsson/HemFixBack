using HemFixBack.Models;

namespace HemFixBack.Services
{
  public interface ITaskService
  {
    public ITask Create(string tableName, ITask task);
    public ITask Get(string tableName, string id);
    public List<ITask> List(string tableName);
    public ITask Update(string tableName, ITask task);
    public bool Delete(string tableName, string id);
  }
}
