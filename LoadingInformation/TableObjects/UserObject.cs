using Npgsql;
using System.Diagnostics.CodeAnalysis;

public class UserObject : TableObject, IIdentifiable
{
    public long Id { get; set; }
    public required string Fullname { get; set; }
    public required string Email { get; set; }

    [SetsRequiredMembers]
    public UserObject(string fullname, string email) =>
        (Fullname, Email) = (fullname, email);

    public NpgsqlCommand GetIdCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT id FROM users WHERE fullname=@fullname AND email=@email;");
        command.Parameters.AddWithValue("fullname", Fullname);
        command.Parameters.AddWithValue("email", Email);

        return command;
    }

    public override NpgsqlCommand GetInsertCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users (fullname, email) VALUES (@fullname, @email);");
        command.Parameters.AddWithValue("fullname", Fullname);
        command.Parameters.AddWithValue("email", Email);

        return command;
    }
}