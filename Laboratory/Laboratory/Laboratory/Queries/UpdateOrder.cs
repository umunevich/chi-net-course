using Npgsql;

namespace Laboratory.Queries;

public static class UpdateOrder
{
    public static void ExecuteWithCommand(string connectionString)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string query = @"
                UPDATE ""Orders""
                SET ord_datetime = 
                    MAKE_TIMESTAMP(
                        @newYear,
                        EXTRACT(MONTH FROM ord_datetime)::int,
                        EXTRACT(DAY FROM ord_datetime)::int,
                        EXTRACT(HOUR FROM ord_datetime)::int,
                        EXTRACT(MINUTE FROM ord_datetime)::int,
                        EXTRACT(SECOND FROM ord_datetime)
                    )
                WHERE ord_datetime >= NOW() - INTERVAL '5 years';
            ";

            using (var command = new NpgsqlCommand(query, connection))
            {
                int newYear = 2024;
                command.Parameters.AddWithValue("@newYear", newYear);

                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"        {rowsAffected} rows affected.");
            }
        }
    }


    public static void ExecuteWithEf(LaboratoryDbContext context)
    {
        var newYear = 2024;
        var fiveYearsAgo = DateTime.UtcNow.AddYears(-5);

        var ordersToUpdate = context.Orders
            .Where(o => o.DateTime >= fiveYearsAgo)
            .ToList();
        
        foreach (var order in ordersToUpdate)
        {
            Console.WriteLine($"{order.Id}: {order.DateTime:yyyy-MM-dd HH:mm:ss}");
            var original = order.DateTime;
            
            order.DateTime = new DateTime(
                newYear,
                original.Month,
                original.Day,
                original.Hour,
                original.Minute,
                original.Second,
                original.Kind == DateTimeKind.Utc ? DateTimeKind.Utc : DateTimeKind.Unspecified
            );
        }
        int rowsAffected = context.SaveChanges();

        Console.WriteLine($"        {rowsAffected} rows affected.");
    }

}