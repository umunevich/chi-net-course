using Laboratory.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laboratory;
public class LaboratoryDbContext : DbContext
{
    public LaboratoryDbContext(DbContextOptions<LaboratoryDbContext> options) : base(options) {}

    public DbSet<Group> Groups { get; set; }
    public DbSet<Analysis> Analyses { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("gr_id");
            entity.Property(e => e.Id).HasColumnName("gr_id");
            entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(255)").HasColumnName("gr_name");
            entity.Property(e => e.Temp).IsRequired().HasColumnType("varchar(100)").HasColumnName("gr_temp");
        });

        modelBuilder.Entity<Analysis>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("an_id");
            entity.Property(e => e.Id).HasColumnName("an_id");
            entity.Property(e => e.Name).IsRequired().HasColumnType("varchar(255)").HasColumnName("an_name");
            entity.Property(e => e.Cost).IsRequired().HasColumnType("numeric(10,2)").HasColumnName("an_cost");
            entity.Property(e => e.Price).IsRequired().HasColumnType("numeric(10,2)").HasColumnName("an_price");
            entity.Property(e => e.GroupId).IsRequired().HasColumnName("an_group");
            
            entity.HasOne(e => e.Group)
                .WithMany(g => g.Analyses)
                .HasForeignKey(e => e.GroupId)
                .HasConstraintName("an_group")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ord_id");
            entity.Property(e => e.Id).HasColumnName("ord_id");
            entity.Property(e => e.DateTime).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnName("ord_datetime");
            entity.Property(e => e.AnalysisId).HasColumnName("ord_an");
            entity.HasOne(e => e.Analysis)
                .WithMany(a => a.Orders)
                .HasForeignKey(e => e.AnalysisId)
                .HasConstraintName("ord_an")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}