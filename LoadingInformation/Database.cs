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

        int id = reader.Read() ? reader.GetInt32(0) : -1;

        _connection.Close();

        return id;
    }

    public void InsertOrdersList((List<OrderObject> ordersList, List<PositionObject> positionsList) orders)
    {
        foreach (OrderObject order in orders.ordersList) 
            Insert(order);

        foreach (PositionObject position in orders.positionsList)
            Insert(position);
    }
}