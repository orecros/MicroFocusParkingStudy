using System.Data;
using System.Data.SqlClient;

public class dbConnection
{
  public SqlConnection conn;
  public SqlTransaction trans;
  
  public dbConnection()
  {
    string serverName;
    string databaseName;
    string strconn = "data source=" + serverName + ";PersistSecurityInfo=true;database=" + databaseName;
    conn = new SqlConnection(strconn);
  }
  
  public void openConnection()
  {
    conn.Close();
    conn.Open();
    trans = conn.BeginTransaction();
  }
  
  public void closeConnection()
  {
    trans.Commit();
    conn.Close();
  }
}
