using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Accountant.Models;
using Accountant.Services;

namespace Accountant.Controllers;

public class HomeController : Controller
{
    private readonly ExpenseService _expenseService;

    public HomeController(ExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    public async Task<IActionResult> Index(int? year, int? month)
    {
        if (year == null || month == null)
        {
            var now = DateTime.Now;
            year = now.Year;
            month = now.Month;
        }

        var expenses = await _expenseService.GetExpensesByMonthAsync(year.Value, month.Value);
    
        ViewBag.SelectedYear = year;
        ViewBag.SelectedMonth = month;

        return View(expenses);
    }

}