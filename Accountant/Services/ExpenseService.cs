using Accountant.Models;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Services;

public class ExpenseService
{
    private readonly AccountantDbContext _context;

    public ExpenseService(AccountantDbContext context)
    {
        _context = context;
    }

    public async Task<List<Expense>> GetAllExpensesWithCategoriesAsync()
    {
        return await _context.Expenses.Include(e => e.Category).ToListAsync();
    }

    public async Task AddExpenseAsync(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
    }
    
    public async Task<List<Expense>> GetExpensesByMonthAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
        var endDate = startDate.AddMonths(1);

        var expenses = await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= startDate && e.Date < endDate)
            .ToListAsync();

        return expenses;
    }
}