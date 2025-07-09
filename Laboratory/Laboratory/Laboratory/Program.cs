using Laboratory;
using Laboratory.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString = config.GetConnectionString("Default");
        
        var options = new DbContextOptionsBuilder<LaboratoryDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        using var context = new LaboratoryDbContext(options);

        var maxRetries = 10;
        var delay = 2000;
        var connected = false;

        for (int i = 0; i < maxRetries; i++)
        {
            Console.WriteLine($"Checking DB connection... Attempt {i + 1}/{maxRetries}");
            if (context.Database.CanConnect())
            {
                connected = true;
                Console.WriteLine("Connected to the database!");
                break;
            }

            Thread.Sleep(delay);
        }

        if (!connected)
        {
            Console.WriteLine("Failed to connect to the database.");
            return;
        }
        
        DbInitializer.Initialize(context);
        
        FirstTask(connectionString, context);
        SecondTask(connectionString, context);
        ThirdTask(connectionString, context);
        //FourthTask(connectionString, context);
    }

    private static void FirstTask(string connectionString, LaboratoryDbContext context)
    {
        Console.WriteLine($"1. All orders for last 5 year:");
        Console.WriteLine($"    1.1. Using NpgsqlCommand and NpgsqlDataReader");
        SelectLastYearOrders.ExecuteWithCommandAndReader(connectionString);
        Console.WriteLine($"    1.2. Using NpgsqlDataAdapter and DataSet");
        SelectLastYearOrders.ExecuteWithAdapterAndSet(connectionString);
        Console.WriteLine($"    1.3. Using EF");
        SelectLastYearOrders.ExecuteWithEf(context);
    }

    private static void SecondTask(string connectionString, LaboratoryDbContext context)
    {
        Console.WriteLine($"2. Insert order:");
        Console.WriteLine($"    2.1. Using NpgsqlCommand");
        InsertOrder.ExecuteWithCommand(connectionString);
        Console.WriteLine($"    2.2. Using EF");
        InsertOrder.ExecuteWithEf(context);
    }

    private static void ThirdTask(string connectionString, LaboratoryDbContext context)
    {
        Console.WriteLine($"3. Update orders that is in interval of 5 years to be made in 2024:");
        Console.WriteLine($"    3.1. Using NpgsqlCommand");
        UpdateOrder.ExecuteWithCommand(connectionString);
        Console.WriteLine($"    3.2. Using EF");
        UpdateOrder.ExecuteWithEf(context);
    }

    private static void FourthTask(string connectionString, LaboratoryDbContext context)
    {
        Console.WriteLine($"4. Delete orders that has ord_ad = @id:");
        Console.WriteLine($"    3.1. Using NpgsqlCommand @id = 1");
        DeleteOrder.ExecuteWithCommand(connectionString);
        Console.WriteLine($"    3.2. Using EF @id = 2");
        DeleteOrder.ExecuteWithEf(context);
    }
}