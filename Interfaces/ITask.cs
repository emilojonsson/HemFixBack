using Npgsql;

namespace HemFixBack.Interfaces
{
  public interface ITask : IDatabase
  {
    string CategoryName { get; set; }
    string Id { get; set; }
    string Title { get; set; }

    void SetAdditionalProperties(NpgsqlDataReader dataReader);
  }
}
