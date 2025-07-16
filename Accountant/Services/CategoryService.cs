using Accountant.Models;
using Microsoft.EntityFrameworkCore;

namespace Accountant.Services;

public class CategoryService
{
    private readonly AccountantDbContext _context;
    
    public CategoryService(AccountantDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateCategoryAsync(Category category)
    {
        var existingCategory = await _context.Categories.FindAsync(category.Id);
        if (existingCategory == null)
        {
            return false;
        }

        existingCategory.Name = category.Name;

        _context.Categories.Update(existingCategory);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}