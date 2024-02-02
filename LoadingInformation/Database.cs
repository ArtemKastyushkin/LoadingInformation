using Npgsql;
using System.Data;

public class Database
{
    private string _connectionString;
    private NpgsqlConnection _connection;
    private NpgsqlCommand _command;

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
}