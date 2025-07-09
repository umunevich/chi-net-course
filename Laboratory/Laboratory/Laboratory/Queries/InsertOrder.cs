using Laboratory.Entities;
using Npgsql;

namespace Laboratory.Queries;

public static class InsertOrder
{
    public static void ExecuteWithCommand(string connectionString)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string query = @"INSERT INTO ""Orders"" (ord_datetime, ord_an) VALUES (@ordDateTime, @ordAn)";
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ordDateTime", DateTime.Now);
                command.Parameters.AddWithValue("@ordAn", 2);
                
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"        {rowsAffected} rows affected.");
            }
        }
    }

    public static void ExecuteWithEf(LaboratoryDbContext context)
    {
        var analysis = context.Analyses.FirstOrDefault(x => x.Id == 2);

        var order = new Order {Id = 0, DateTime = DateTime.UtcNow, Analysis = analysis};
        var entity = context.Orders.Add(order);
        Console.WriteLine($"        State after adding: {entity.State}");
        context.SaveChanges();
        Console.WriteLine($"        State after SaveChanges(): {entity.State}");
    }
}