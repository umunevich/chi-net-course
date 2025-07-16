using Accountant.Models;
using Microsoft.EntityFrameworkCore;

namespace Accountant;

public class AccountantDbContext : DbContext
{
    public AccountantDbContext(DbContextOptions<AccountantDbContext> options) : base(options) { }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Category");
            entity.Property(e => e.Name).HasColumnType("varchar(255)").IsRequired();
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Expense");
            entity.Property(e => e.Amount).HasColumnType("money").IsRequired();
            entity.Property(e => e.Comment).HasColumnType("varchar(255)");
            entity.Property(e => e.CategoryId).IsRequired();
            modelBuilder.Entity<Expense>()
                .Property(e => e.Date)
                .HasColumnType("timestamp with time zone");
            entity.HasOne(e => e.Category).WithMany(c => c.Expenses).HasForeignKey(e => e.CategoryId);
        });
    }
}
