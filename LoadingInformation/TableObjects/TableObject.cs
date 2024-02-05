using Npgsql;

public abstract class TableObject
{
    public abstract NpgsqlCommand GetInsertCommand();
} 