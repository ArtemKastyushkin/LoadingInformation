using Npgsql;
using System.Diagnostics.CodeAnalysis;

public class ProductObject : TableObject, IIdentifiable
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }

    [SetsRequiredMembers]
    public ProductObject(string name, decimal price) =>
        (Name, Price) = (name, price);

    public NpgsqlCommand GetIdCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("SELECT id FROM products WHERE name=@name AND price=@price;");
        command.Parameters.AddWithValue("name", Name);
        command.Parameters.AddWithValue("price", Price);

        return command;
    }

    public override NpgsqlCommand GetInsertCommand()
    {
        NpgsqlCommand command = new NpgsqlCommand("INSERT INTO products (name, price) VALUES (@name, @price);");
        command.Parameters.AddWithValue("name", Name);
        command.Parameters.AddWithValue("price", Price);

        return command;
    }
}