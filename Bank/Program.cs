namespace Bank;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the amount you want to balance: ");
        decimal.TryParse(Console.ReadLine(), out decimal amount);
        var deposit = new Deposit(amount);
        Console.WriteLine($"Percentage: {deposit.Percentage}");
        Console.WriteLine($"Total amount after applying percentage: {deposit.CalculateTotalWithPercentage()}");
        Console.WriteLine($"With bonuses: {deposit.CalculateTotalWithPercentageAndBonus()}");
    }
}