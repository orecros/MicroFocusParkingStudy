using System.Data;
using System.Data.SqlClient;

public class dbConnection
{
  public SqlConnection conn;
  
  public dbConnection()
  {
    string serverName;
    string databaseName;
    string userID;
    string password;
    string strconn = "data source=" + serverName + ";PersistSecurityInfo=true;database=" + databaseName + ";userid=" + userID
      + ";password=" + password;
    conn = new SqlConnection(strconn);
  }
  
  public void addParkingLot(string name, string camID, string street, string city, string zip)
  {
    string query = "INSERT INTO PARKING_LOT (Lot_name, Camera_id, Street, City, Zip_code) VALUES (@Lot_name, @Camera_id, @Street, @City, @Zip_code)";
    SqlCommand cmd = new SqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@Lot_name", name);
    cmd.Parameters.AddWithValue("@Camera_id", camID);
    cmd.Parameters.AddWithValue("@Street", street);
    cmd.Parameters.AddWithValue("@City", city);
    cmd.Parameters.AddWithValue("@Zip_code", zip);
    insertRecord(cmd);
  }
  
  public void addParkingSpace(int pointID, double xCoord, double yCoord, double width, double height)
  {
    string query = "INSERT INTO PARKING_SPACE (Space_point_id, x_space, y_space, Space_width, Space_height) VALUES (@Space_point_id, @x_space, @y_space, @Space_width, @Space_height)";
    SqlCommand cmd = new SqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@Space_point_id", pointID);
    cmd.Parameters.AddWithValue("@x_space", xCoord);
    cmd.Parameters.AddWithValue("@y_space", yCoord);
    cmd.Parameters.AddWithValue("@Space_width", width);
    cmd.Parameters.AddWithValue("@Space_height", height);
    insertRecord(cmd);
  }
  
  public void addVehicle(int pointID, double xCoord, double yCoord, double width, double height, DateTime parked, DateTime vacant, string vType)
  {
    string query = "INSERT INTO PARKING_SPACE (Space_point_id, x_space, y_space, Space_width, Space_height, Parked_datetime, Vacated_datetime, Vehicle_type) VALUES (@Space_point_id, @x_space, @y_space, @Space_width, @Space_height, @Parked_datetime, @Vacated_datetime, @Vehicle_type)";
    SqlCommand cmd = new SqlCommand(query, conn);
    cmd.Parameters.AddWithValue("@Space_point_id", pointID);
    cmd.Parameters.AddWithValue("@x_space", xCoord);
    cmd.Parameters.AddWithValue("@y_space", yCoord);
    cmd.Parameters.AddWithValue("@Space_width", width);
    cmd.Parameters.AddWithValue("@Space_height", height);
    cmd.Parameters.AddWithValue("@Parked_datetime", parked);
    cmd.Parameters.AddWithValue("@Vacated_datetime", vacant);
    cmd.Parameters.AddWithValue("@Vehicle_type", vType);
    insertRecord(cmd);
  }
  
  private void insertRecord(SqlCommand cmd)
  {
    try
    {
      conn.Close();
      conn.Open();
      cmd.ExecuteNonQuery();
    }
    catch (SqlException e)
    {
      Console.WriteLine("Error Generated. Details: " + e.ToString());
    }
    finally
    {
      conn.Close();
    }
  }
}

