using Npgsql;

namespace Laboratory.Queries;

public static class DeleteOrder
{
    public static void ExecuteWithCommand(string connectionString)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string query = @"DELETE FROM ""Orders"" WHERE ord_an = @ordAn";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ordAn", 1);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"        {rowsAffected} rows affected.");
            }
        }
    }

    public static void ExecuteWithEf(LaboratoryDbContext context)
    {
        var ordersToDelete = context.Orders.Where(o => o.AnalysisId == 2).ToList();

        context.Orders.RemoveRange(ordersToDelete);
        int rowsAffected = context.SaveChanges();

        Console.WriteLine($"        {rowsAffected} rows affected.");
    }

}