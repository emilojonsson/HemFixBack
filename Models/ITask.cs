namespace HemFixBack.Models
{
  public interface ITask
  {
    string CategoryName { get; set; }
    string Id { get; set; }
    string Title { get; set; }
    string ValueString();
  }
}
