namespace HemFixBack.Models
{
  public class GardenTask : Task
  {
    public string? Exposure { get; set; }
    public string? MaxZone { get; set; }
    public string? MinZone { get; set; }
    public string? PlantingDistance { get; set; }
    public string? Prune { get; set; }
    public string? Soil { get; set; }
  }
}
