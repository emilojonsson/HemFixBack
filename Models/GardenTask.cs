using Npgsql;

namespace HemFixBack.Models
{
  public class GardenTask : Task
  {
    public string? Description { get; set; }
    public string? Exposure { get; set; }
    public int Interval { get; set; }
    public string? MaxZone { get; set; }
    public string? Month { get; set; }
    public string? MinZone { get; set; }
    public string? PlantingDistance { get; set; }
    public string? ReminderType { get; set; }
    public string? Soil { get; set; }

    public override void SetAdditionalProperties(NpgsqlDataReader dataReader)
    {
      Title = dataReader["gardentask_title"] as string;
      Id = dataReader["gardentask_id"] as string;
      Priority = Convert.ToBoolean(dataReader["gardentask_priority"]);
      CategoryName = dataReader["gardentask_categoryname"] as string;

      Description = dataReader["gardentask_description"] as string;
      Exposure = dataReader["gardentask_exposure"] as string;
      Interval = Convert.ToInt32(dataReader["gardentask_interval"]);
      MaxZone = dataReader["gardentask_maxZone"] as string;
      Month = dataReader["gardentask_month"] as string;
      MinZone = dataReader["gardentask_minZone"] as string;
      PlantingDistance = dataReader["gardentask_plantingDistance"] as string;
      ReminderType = dataReader["gardentask_reminderType"] as string;
      Soil = dataReader["gardentask_soil"] as string;
    }
  }
}
