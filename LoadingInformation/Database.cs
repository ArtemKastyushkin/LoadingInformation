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

    private NpgsqlDataReader? select(TableObject tableObject)
    {
        connect();

        NpgsqlCommand command = tableObject.GetSelectCommand();
        command.Connection = _connection;

        NpgsqlDataReader? reader = null;

        try
        {
            reader = command.ExecuteReader();
        }
        catch (NpgsqlException ex)
        {
            Console.WriteLine(ex.Message);
            _connection.Close();
        }

        return reader;
    }

    public Database(ConnectionConfig connectionConfig)
    {
        _connection = new NpgsqlConnection(connectionConfig.GetConnectionString());
    }

    public int Insert(TableObject tableObject)
    {
        connect();

        NpgsqlCommand command = tableObject.GetInsertCommand();
        command.Connection = _connection;

        int affectedRowsCount = 0;

        try
        {
            affectedRowsCount = command.ExecuteNonQuery();
        }
        catch (NpgsqlException ex) 
        { 
            Console.WriteLine(ex.Message);
            _connection.Close();
        }

        return affectedRowsCount;
    }

    public long GetId(TableObject tableObject)
    {
        NpgsqlDataReader? reader = select(tableObject);

        if (reader == null)
            return -1;

        long id = -1;
        reader.Read();

        try
        {
            id = Convert.ToInt64(reader["id"]);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            _connection.Close();
        }

        reader.Close();

        return id;
    }

    public bool IsExist(TableObject tableObject)
    {
        NpgsqlDataReader? reader = select(tableObject);

        if (reader == null)
            return false;

        bool isExist = reader.Read();

        reader.Close();

        return isExist;
    }

    public void InsertOrdersList((List<OrderObject> ordersList, List<PositionObject> positionsList) orders)
    {
        foreach (OrderObject order in orders.ordersList) 
            Insert(order);

        foreach (PositionObject position in orders.positionsList)
            Insert(position);
    }
}