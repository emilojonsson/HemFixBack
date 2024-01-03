using HemFixBack.Models;

namespace HemFixBack.Services
{
  public interface IPurchaseTaskService
  {
    public PurchaseTask Create(PurchaseTask task);
    public PurchaseTask Get(string id);
    public List<PurchaseTask> List();
    public PurchaseTask Update(PurchaseTask task);
    public bool Delete(string id);
  }
}
