using Laboratory.Entities;

namespace Laboratory;

public static class DbInitializer
{
    public static void Initialize(LaboratoryDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        var groups = new List<Group>
        {
            new Group { Name = "Гематологія", Temp = "2-8°C" },
            new Group { Name = "Біохімія", Temp = "2-8°C" },
            new Group { Name = "Імунологія", Temp = "холодильник" },
            new Group { Name = "ПЛР-діагностика", Temp = "-20°C" },
            new Group { Name = "Мікробіологія", Temp = "кімнатна температура" }
        };

        context.Groups.AddRange(groups);
        context.SaveChanges();
        
        var analyses = new List<Analysis>
        {
            new Analysis { Name = "Загальний аналіз крові", Cost = 50, Price = 150, Group = groups[0] },
            new Analysis { Name = "Глюкоза", Cost = 20, Price = 80, Group = groups[1] },
            new Analysis { Name = "АЛТ", Cost = 25, Price = 90, Group = groups[1] },
            new Analysis { Name = "Антитіла до COVID-19 (IgG)", Cost = 60, Price = 250, Group = groups[2] },
            new Analysis { Name = "ПЛР на COVID-19", Cost = 100, Price = 400, Group = groups[3] },
            new Analysis { Name = "Мазок на стафілокок", Cost = 30, Price = 120, Group = groups[4] }
        };

        context.Analyses.AddRange(analyses);
        context.SaveChanges();
        
        DateTime Utc(string dateTime) => DateTime.SpecifyKind(DateTime.Parse(dateTime), DateTimeKind.Utc);
        
        var orders = new List<Order>
        {
            new Order { DateTime = Utc("2020-01-10 09:30"), Analysis = analyses[0] },
            new Order { DateTime = Utc("2020-01-15 11:00"), Analysis = analyses[1] },
            new Order { DateTime = Utc("2020-01-20 10:15"), Analysis = analyses[0] },
            new Order { DateTime = Utc("2020-02-05 12:00"), Analysis = analyses[2] },
            new Order { DateTime = Utc("2020-02-10 13:45"), Analysis = analyses[3] },
            new Order { DateTime = Utc("2020-02-12 14:20"), Analysis = analyses[1] },
            new Order { DateTime = Utc("2020-02-15 15:30"), Analysis = analyses[0] },
            new Order { DateTime = Utc("2020-03-01 09:00"), Analysis = analyses[4] },
            new Order { DateTime = Utc("2020-03-10 10:00"), Analysis = analyses[3] },
            new Order { DateTime = Utc("2020-03-15 11:30"), Analysis = analyses[5] },
            new Order { DateTime = Utc("2020-03-20 12:45"), Analysis = analyses[0] },
            new Order { DateTime = Utc("2021-01-05 08:20"), Analysis = analyses[0] },
            new Order { DateTime = Utc("2021-01-10 10:50"), Analysis = analyses[2] },
            new Order { DateTime = Utc("2021-02-07 09:40"), Analysis = analyses[1] },
            new Order { DateTime = Utc("2021-02-12 11:10"), Analysis = analyses[4] },
            new Order { DateTime = Utc("2021-03-03 10:15"), Analysis = analyses[5] },
            new Order { DateTime = Utc("2021-03-10 13:30"), Analysis = analyses[5] }
        };

        context.Orders.AddRange(orders);
        context.SaveChanges();
    }
}
