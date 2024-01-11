using Npgsql;

namespace HemFixBack.Models
{
  public class PurchaseTask : Task
  {
    public override void SetAdditionalProperties(NpgsqlDataReader dataReader)
    {
      Title = dataReader["purchasetask_title"] as string;
      Id = dataReader["purchasetask_id"] as string;
      CategoryName = dataReader["purchasetask_categoryname"] as string;
    }

  }
}
