namespace Laboratory.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Temp { get; set; }
    public List<Analysis> Analyses { get; set; } = new List<Analysis>();
}