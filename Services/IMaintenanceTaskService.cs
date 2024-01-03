using HemFixBack.Models;

namespace HemFixBack.Services
{
  public interface IMaintenanceTaskService
  {
    public MaintenanceTask Create(MaintenanceTask task);
    public MaintenanceTask Get(string id);
    public List<MaintenanceTask> List();
    public MaintenanceTask Update(MaintenanceTask task);
    public bool Delete(string id);
  }
}
