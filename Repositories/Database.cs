using System.Data;
using HemFixBack.Models;
using Npgsql;

namespace HemFixBack.Repositories
{

  public static class Database
  {
    public static Dictionary<string, Type> TypeOfClass { get; } =
      //a "key" of all classes that implement the database
      new Dictionary<string, Type>
      {
        { "simpletask", typeof(SimpleTask) },
        { "gardentask", typeof(GardenTask) },
        { "maintenancetask", typeof(MaintenanceTask) },
        { "purchasetask", typeof(PurchaseTask) }
      };

    public static NpgsqlConnection GetConnection()
    {
      string password = Environment.GetEnvironmentVariable("passwordPG");
      return new NpgsqlConnection(
        $@"Server=localhost;Port=5432;User Id=postgres;Password={password};Database=HemFix"
      );
    }

    public static void TestConnection()
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

    public static bool CreateRecord(object theObject)
    {
      if (!(theObject is ITask))
      {
        throw new ArgumentException("Object does not implement ITask interface.");
      }

      using (NpgsqlConnection con = GetConnection())
      {
        var type = theObject.GetType();
        var tableName = TypeOfClass.FirstOrDefault(x => x.Value == type).Key;
        string propertyName = string.Empty;

        foreach (var property in type.GetProperties().OrderBy(p => p.Name))
        {
          propertyName = propertyName + tableName + "_" + property.Name + ",";
        }

        propertyName = propertyName.TrimEnd(',');
        var objectToSave = (ITask)theObject;
        string query =
          $@"INSERT INTO {tableName.ToLower()} ({propertyName.ToLower()}) VALUES({objectToSave.ValueString()});";
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

    public static object[] ReadRecord(string tableName, string id)
    {
      using (NpgsqlConnection con = GetConnection())
      {
        var query = $"SELECT * FROM {tableName} WHERE {tableName}_id = '{id}'";
        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        con.Open();
        var dataReader = cmd.ExecuteReader();
        dataReader.Read();
        int columns = dataReader.FieldCount;
        object[] values = new object[columns];
        dataReader.GetValues(values);
        return values;
      }
    }

    public static List<object[]> ListRecords(string tableName)
    {
      using (NpgsqlConnection con = GetConnection())
      {
        var query = $"SELECT * FROM {tableName}";
        NpgsqlCommand cmd = new NpgsqlCommand(query, con);
        con.Open();
        var dataReader = cmd.ExecuteReader();
        List<object[]> records = new List<object[]>();
        while (dataReader.Read())
        {
          int columns = dataReader.FieldCount;
          object[] values = new object[columns];
          dataReader.GetValues(values);
          records.Add(values);
        }
        return records;
      }
    }

    public static bool DeleteRecord(string tableName, string id)
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
