using MySqlConnector;

public class DatabaseManager
{
    private static MySqlConnection _connection;

    public static MySqlConnection GetConnection()
    {
        if (_connection == null)
        {
            string connectionString = "Server=localhost;User ID=root;Password=Kn200499;Database=studentmanagement";
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
        }

        return _connection;
    }

    public static void CloseConnection()
    {
        if (_connection != null)
        {
            _connection.Close();
            _connection = null;
        }
    }
}
