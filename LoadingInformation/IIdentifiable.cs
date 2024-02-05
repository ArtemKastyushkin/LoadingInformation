using Npgsql;

public interface IIdentifiable
{
    public NpgsqlCommand GetIdCommand();
}