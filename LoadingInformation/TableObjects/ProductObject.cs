using Npgsql;
using System.Diagnostics.CodeAnalysis;

public class ProductObject : TableObject
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    [SetsRequiredMembers]
    public ProductObject(string name, decimal price) =>
        (Name, Price) = (name, price);

    public override NpgsqlCommand GetInsertCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO products (name, price) VALUES (@name, @price);");
        command.Parameters.AddWithValue("name", Name);
        command.Parameters.AddWithValue("price", Price);

        return command;
    }

    public override NpgsqlCommand GetSelectCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT id, name, price FROM products WHERE name=@name AND price=@price;");
        command.Parameters.AddWithValue("name", Name);
        command.Parameters.AddWithValue("price", Price);

        return command;
    }
}