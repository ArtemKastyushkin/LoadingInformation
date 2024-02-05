using Npgsql;
using System.Data;

public class Database
{
    private NpgsqlConnection _connection;

    private void connect()
    {
        if (_connection.State == ConnectionState.Closed)
            _connection.Open();
    }

    public Database(ConnectionConfig connectionConfig)
    {
        _connection = new NpgsqlConnection(connectionConfig.GetConnectionString());
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

    public int GetId(IIdentifiable identifiableTableObject)
    {
        connect();

        NpgsqlCommand command = identifiableTableObject.GetIdCommand();
        command.Connection = _connection;

        NpgsqlDataReader reader;

        try
        {
            reader = command.ExecuteReader();
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            _connection.Close();

            return -1;
        }

        return reader.Read() ? reader.GetInt32(0) : -1;
    }
}