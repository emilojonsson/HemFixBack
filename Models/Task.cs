using HemFixBack.Interfaces;
using System.Text;

namespace HemFixBack.Models
{
  public abstract class Task : ITask
  {
    public string Background { get; set; }
    public string CategoryName { get; set; }
    public string Id { get; set; }
    public bool Priority { get; set; }
    public int TaskIndex { get; set; }
    public string Title { get; set; }

    public abstract void SetAdditionalProperties(Npgsql.NpgsqlDataReader dataReader);

    public string ValueString()
    {
      StringBuilder resultBuilder = new StringBuilder();
      foreach (var property in this.GetType().GetProperties().OrderBy(p => p.Name))
      {
        if (property.PropertyType == typeof(string))
        {
          resultBuilder.Append($"'{property.GetValue(this)}',");
        }
        else
        {
          if (property.GetValue(this) != null) 
          {
            resultBuilder.Append($"{property.GetValue(this)},");
          }
          else 
          {
            resultBuilder.Append("DEFAULT");
          }
        }
      }
      return resultBuilder.ToString().TrimEnd(',').ToLower();
    }
  }
}
