namespace Laboratory.Entities;

public class Order
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    
    public int AnalysisId { get; set; }
    public Analysis Analysis { get; set; } = null!;
}