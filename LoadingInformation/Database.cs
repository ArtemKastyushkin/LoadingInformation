using Npgsql;
using System.Data;

public class Database
{
    private string _connectionString;
    private NpgsqlConnection _connection;

    private void connect()
    {
        _connection = new NpgsqlConnection();
        _connection.ConnectionString = _connectionString;

        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
    }

    public Database(ConnectionConfig connectionConfig)
    {
        _connectionString = connectionConfig.GetConnectionString();

        connect();
    }

    public void Insert(TableObject tableObject)
    {
        connect();

        NpgsqlCommand command = tableObject.GetInsertCommand();
        command.Connection = _connection;

        try
        {
            command.ExecuteNonQuery();
        }
        catch (NpgsqlException ex) 
        { 
            Console.WriteLine(ex.Message);
            _connection.Close();
        }
    }
}