namespace Algos;

class Program
{
    static void Main(string[] args)
    {
        FirstTask("../../../TextFiles/FirstFile.txt");
        SecondTask("../../../TextFiles/SecondFile.txt");
        ThirdTask("../../../TextFiles/ThirdFile.txt");
        FourthTask("../../../TextFiles/FourthFile.txt");
    }

    static void FirstTask(string fileName)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string text = File.ReadAllText(fileName);
            Console.WriteLine("1. Sample text:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
            
            var result = text.Where(x => char.IsDigit(x))
                .Select(x => int.Parse(x.ToString()))
                .ToList();
            
            Console.WriteLine("Sum of digits in text: " + result.Sum());
            Console.WriteLine("Maximum digit: " + (result.Capacity != 0? result.Max() : "there is no digits"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    static void SecondTask(string fileName)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string text = File.ReadAllText(fileName);
            Console.WriteLine("2. Sample text:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
            
            string trimmed = text.TrimStart();
            int index = trimmed
                .Select((ch, i) => new { ch, i })
                .Where(x => char.IsDigit(x.ch))
                .OrderByDescending(x => x.ch)
                .FirstOrDefault()?.i ?? -1;
            Console.WriteLine("First maximum digit at " + index + " position");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    
    static void ThirdTask(string fileName)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string text = File.ReadAllText(fileName);
            Console.WriteLine("3. Sample array:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
            
            var result = text.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x.Trim()))
                .ToList();
            
            Console.WriteLine("Amount of pages in the thickest book: " + result.Max());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    static void FourthTask(string fileName)
    {
        try
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string text = File.ReadAllText(fileName);
            Console.WriteLine("3. Sample array:");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Green;
            
            var result = text.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x.Trim()))
                .ToList();
            
            int firstIndex = result.Select((num, i) => new { num, i })
                .OrderByDescending(x => x.num)
                .FirstOrDefault()?.i ?? -1;
            
            int lastIndex = result.Select((num, i) => new { num, i })
                .GroupBy(x => x.num)
                .OrderByDescending(x => x.Key)
                .FirstOrDefault()?.Last().i ?? -1;
            
            Console.WriteLine("First index of the quickest car: " + firstIndex);
            Console.WriteLine("Last index of the quickest car: " + lastIndex);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}