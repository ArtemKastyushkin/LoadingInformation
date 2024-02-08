using Npgsql;
using System.Diagnostics.CodeAnalysis;

public class OrderObject : TableObject
{
    public required long Id { get; set; }
    public required long UserId { get; set; }
    public required decimal Sum { get; set; }
    public required DateTime RegDate { get; set; }

    [SetsRequiredMembers]
    public OrderObject(long id, long userId, decimal sum, DateTime regDate) =>
        (Id, UserId, Sum, RegDate) = (id, userId, sum, regDate);

    public override NpgsqlCommand GetInsertCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO orders (id, user_id, sum, reg_date) VALUES (@id, @user_id, @sum, @reg_date);");
        command.Parameters.AddWithValue("id", Id);
        command.Parameters.AddWithValue("user_id", UserId);
        command.Parameters.AddWithValue("sum", Sum);
        command.Parameters.AddWithValue("reg_date", RegDate);

        return command;
    }

    public override NpgsqlCommand GetSelectCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT id, user_id, sum, reg_date FROM orders WHERE user_id=@user_id AND sum=@sum AND reg_date=@reg_date);");
        command.Parameters.AddWithValue("user_id", UserId);
        command.Parameters.AddWithValue("sum", Sum);
        command.Parameters.AddWithValue("reg_date", RegDate);

        return command;
    }
}