using Npgsql;
using System.Data;

namespace Laboratory.Queries;

public static class SelectLastYearOrders
{
    public static void ExecuteWithCommandAndReader(string connectionString)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string query = @"SELECT * FROM ""Orders"" WHERE ord_datetime >= @StartDate";
            
            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                DateTime oneYearAgo = DateTime.Now.AddYears(-5);
                command.Parameters.AddWithValue("@StartDate", oneYearAgo);
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int ordId = reader.GetInt32(0);
                        DateTime ordDateTime = reader.GetDateTime(1);
                        Console.WriteLine($"        Order ID: {ordId}, Order Date: {ordDateTime}");
                    }
                }
            }
        }
    }


    public static void ExecuteWithAdapterAndSet(string connectionString)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
    
            string query = @"SELECT * FROM ""Orders"" WHERE ord_datetime >= NOW() - INTERVAL '5 years'";
    
            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection))
            {
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet, "Orders");

                DataTable ordersTable = dataSet.Tables["Orders"];
                foreach (DataRow row in ordersTable.Rows)
                {
                    int ordId = Convert.ToInt32(row["ord_id"]);
                    DateTime ordDateTime = Convert.ToDateTime(row["ord_datetime"]);
                    Console.WriteLine($"        Order ID: {ordId}, Order Date: {ordDateTime}");
                }
            }
        }

    }

    public static void ExecuteWithEf(LaboratoryDbContext context)
    {
        var sinceDate = DateTime.UtcNow.AddYears(-5);
        var orders = context.Orders.Where(o => o.DateTime >= sinceDate).ToList();
        foreach (var order in orders)
        {
            Console.WriteLine($"        Order ID: {order.Id}, Order Date: {order.DateTime}");
        }
    }
}