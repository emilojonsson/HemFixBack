namespace HemFixBack.Models
{
  public abstract class Task : ITask
  {
    public string CategoryName { get; set; }
    public string Id { get; set; }
    public string Title { get; set; }
    public string ValueString()
    {
      string attributes = string.Empty;
      foreach (var property in this.GetType().GetProperties().OrderBy(p => p.Name))
      {
        if (property.PropertyType == typeof(string))
        {
          attributes += $"'{property.GetValue(this)}',";
        }
        else
        {
          if (property.GetValue(this) != null) 
          {
            attributes += $"{property.GetValue(this)},";
          }
          else 
          {
            attributes += "DEFAULT,";
          }
        }
      }
      return attributes.TrimEnd(',').ToLower();
    }
  }
}
