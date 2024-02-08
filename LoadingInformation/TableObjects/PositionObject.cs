using Npgsql;
using System.Diagnostics.CodeAnalysis;

public class PositionObject : TableObject
{
    public required long ProductId { get; set; }
    public required long OrderId { get; set; }
    public required int Quantity { get; set; }

    [SetsRequiredMembers]
    public PositionObject(long productId, long orderId, int quantity) =>
        (ProductId, OrderId, Quantity) = (productId, orderId, quantity);

    public override NpgsqlCommand GetInsertCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO positions (product_id, order_id, quantity) VALUES (@product_id, @order_id, @quantity);");
        command.Parameters.AddWithValue("product_id", ProductId);
        command.Parameters.AddWithValue("order_id", OrderId);
        command.Parameters.AddWithValue("quantity", Quantity);

        return command;
    }

    public override NpgsqlCommand GetSelectCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT product_id, order_id, quantity FROM positions WHERE product_id=@product_id AND order_id=@order_id AND quantity=@quantity;");
        command.Parameters.AddWithValue("product_id", ProductId);
        command.Parameters.AddWithValue("order_id", OrderId);
        command.Parameters.AddWithValue("quantity", Quantity);

        return command;
    }
}