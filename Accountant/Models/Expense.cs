namespace Accountant.Models;

public class Expense
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => _date = value.Kind == DateTimeKind.Unspecified ? DateTime.SpecifyKind(value, DateTimeKind.Utc) : value;
    }
    public string? Comment { get; set; }

    public Category Category { get; set; } = null!;
}