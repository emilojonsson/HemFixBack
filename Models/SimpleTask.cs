using Npgsql;

namespace HemFixBack.Models
{
  public class SimpleTask : Task
  {
    public override void SetAdditionalProperties(NpgsqlDataReader dataReader)
    {
      Title = dataReader["simpletask_title"] as string;
      Id = dataReader["simpletask_id"] as string;
      CategoryName = dataReader["simpletask_categoryname"] as string;
    }

  }
}
