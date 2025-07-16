using Accountant.Models;
using Accountant.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.Controllers;

[Controller]
[Route("[controller]")]
public class ExpenseController : Controller
{
    private readonly ExpenseService _expenseService;
    private readonly CategoryService _categoryService;

    public ExpenseController(ExpenseService expenseService, CategoryService categoryService)
    {
        _expenseService = expenseService;
        _categoryService = categoryService;
    }

    [HttpGet("Add")]
    public async Task<IActionResult> Add()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromForm] Expense expense)
    {
        var category = await _categoryService.GetCategoryByIdAsync(expense.CategoryId);
        if (category is not null) expense.Category = category;
        
        ModelState.Clear();
        TryValidateModel(expense);
        
        if (ModelState.IsValid)
        {
            await _expenseService.AddExpenseAsync(expense);
            return RedirectToAction("Index", "Home");
        }
        var categories = await _categoryService.GetAllCategoriesAsync();
        ViewBag.Categories = categories;
        return View(expense);
    }
}