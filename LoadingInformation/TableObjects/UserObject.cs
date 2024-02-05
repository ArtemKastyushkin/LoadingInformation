using Npgsql;

public class UserObject : TableObject
{
    public long Id { get; set; }
    public string Fullname { get; set; }
    public string Email { get; set; }

    public override NpgsqlCommand GetInsertCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users (fullname, email) VALUES (@fullname, @email);");
        command.Parameters.AddWithValue("fullname", Fullname);
        command.Parameters.AddWithValue("email", Email);

        return command;
    }
}