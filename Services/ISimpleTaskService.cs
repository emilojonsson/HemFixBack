using HemFixBack.Models;

namespace HemFixBack.Services
{
  public interface ISimpleTaskService
  {
    public SimpleTask Create(SimpleTask task);
    public SimpleTask Get(string id);
    public List<SimpleTask> List();
    public SimpleTask Update(SimpleTask task);
    public bool Delete(string id);
  }
}
