using System.ComponentModel.DataAnnotations;

namespace Accountant.Models;

public class Category
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Вкажіть назву категорії")]
    public string Name { get; set; } = null!;
    public List<Expense> Expenses { get; set; } = new List<Expense>();
}