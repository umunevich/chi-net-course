using Accountant.Models;
using Accountant.Services;
using Microsoft.AspNetCore.Mvc;

namespace Accountant.Controllers;

[Controller]
[Route("[controller]")]
public class CategoryController : Controller
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return View(categories);
    }

    [HttpGet("Add")]
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost("Add")]
    public async Task<IActionResult> Add([FromForm] Category category)
    {
        if (ModelState.IsValid)
        {
            await _categoryService.AddCategoryAsync(category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }
    
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    
    [HttpPost("Edit/{id}")]
    public async Task<IActionResult> Edit(int id, [FromForm] Category category)
    {
        if (id != category.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            var success = await _categoryService.UpdateCategoryAsync(category);
            if (!success)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }
    
    [HttpPost("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _categoryService.DeleteCategoryAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }
}