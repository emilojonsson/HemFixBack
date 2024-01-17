using Npgsql;

namespace HemFixBack.Interfaces
{
  public interface ITask : IDatabase
  {
    string CategoryName { get; set; }
    string Id { get; set; }
    string Title { get; set; }
    bool Priority { get; set; }
    int TaskIndex { get; set; }
    string Background { get; set; }

    void SetAdditionalProperties(NpgsqlDataReader dataReader);
  }
}
