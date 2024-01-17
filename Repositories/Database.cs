using HemFixBack.Interfaces;
using System.Data;
using System.Text;
using Npgsql;

namespace HemFixBack.Repositories
{
  public class Database
  {
    public NpgsqlConnection GetConnection()
    {
      string userNameDB = Environment.GetEnvironmentVariable("userPG");
      string password = Environment.GetEnvironmentVariable("passwordPG");
      string databaseName = Environment.GetEnvironmentVariable("databasePG");
      return new NpgsqlConnection(
        $@"Server=localhost;Port=5432;User Id={userNameDB};Password={password};Database={databaseName}"
      );
    }

    public void TestConnection()
    {
      using (NpgsqlConnection con = GetConnection())
      {
        con.Open();
        if (con.State == ConnectionState.Open)
        {
          Console.WriteLine("Connected");
        }
      }
    }

    public bool CreateRecord(string tableName, IDatabase newIDatabaseObject)
    {
      using (NpgsqlConnection con = GetConnection())
      {
        StringBuilder resultBuilder = new StringBuilder();
        foreach (var property in newIDatabaseObject.GetType().GetProperties().OrderBy(p => p.Name))
        {
          resultBuilder.Append($"{tableName}_{property.Name},");
        }
        string propertyNames = resultBuilder.ToString().ToLower().TrimEnd(',');
        string query =
          $@"INSERT INTO {tableName.ToLower()} ({propertyNames}) VALUES({newIDatabaseObject.ValueString()});";
        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        con.Open();
        int n = cmd.ExecuteNonQuery();
        if (n < 0)
        {
          return false;
        }
        else
        {
          return true;
        }
      }
    }

    public NpgsqlDataReader ReadRecord(string tableName, string id)
    {
      NpgsqlConnection con = GetConnection();
      con.Open();
      var query = $"SELECT * FROM {tableName} WHERE {tableName}_id = '{id}'";
      NpgsqlCommand cmd = new NpgsqlCommand(query, con);
      return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public NpgsqlDataReader ListRecords(string tableName)
    {
      NpgsqlConnection con = GetConnection();
      con.Open();
      var query = $"SELECT * FROM {tableName} ORDER BY {tableName}_taskindex";
      NpgsqlCommand cmd = new NpgsqlCommand(query, con);
      return cmd.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public bool DeleteRecord(string tableName, string id)
    {
      using (NpgsqlConnection con = GetConnection())
      {
        var query = $@"DELETE FROM {tableName} WHERE {tableName}_id = '{id}';";
        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        con.Open();
        int n = cmd.ExecuteNonQuery();
        if (n > 0)
        {
          return true;
        }
        return false;
      }
    }
  }
}
