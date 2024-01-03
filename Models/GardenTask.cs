namespace HemFixBack.Models
{
  public class GardenTask : Task
  {
    public string CategoryName { get; set; }
    public string? Exposure { get; set; }
    public string Id { get; set; }
    public string? MaxZone { get; set; }
    public string? MinZone { get; set; }
    public string? PlantingDistance { get; set; }
    public string? Prune { get; set; }
    public string? Soil { get; set; }
    public string Title { get; set; }
  }
}
