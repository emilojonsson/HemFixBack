using Npgsql;

namespace HemFixBack.Models
{
  public class MaintenanceTask : Task
  {
    public override void SetAdditionalProperties(NpgsqlDataReader dataReader)
    {
      Title = dataReader["maintenancetask_title"] as string;
      Id = dataReader["maintenancetask_id"] as string;
      CategoryName = dataReader["maintenancetask_categoryname"] as string;
    }
  }
}
