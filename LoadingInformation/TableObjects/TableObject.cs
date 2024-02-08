using Npgsql;

public abstract class TableObject
{
    public abstract NpgsqlCommand GetInsertCommand();
    public abstract NpgsqlCommand GetSelectCommand();
} 