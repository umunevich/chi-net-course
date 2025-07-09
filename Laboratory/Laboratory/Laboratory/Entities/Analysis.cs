namespace Laboratory.Entities;

public class Analysis
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Cost { get; set; }
    public decimal Price { get; set; }
    
    public int GroupId { get; set; }
    public Group Group { get; set; } = null!;
    
    public List<Order> Orders { get; set; } = new List<Order>();
}