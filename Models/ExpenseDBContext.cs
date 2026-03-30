using Microsoft.EntityFrameworkCore;

namespace expense_tracker.Models;

public class ExpenseDBContext : DbContext
{
    public ExpenseDBContext(DbContextOptions<ExpenseDBContext> options)
    : base(options) {}

    public DbSet<Credentials> Credentials {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Credentials>(entity =>
        {
            entity.ToTable("credentials");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id");

            entity.Property(p => p.Username)
                .HasColumnName("username")
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.Email)
                .HasColumnName("email")
                .HasMaxLength(300);

            entity.Property(p => p.HashedPassword)
                .HasColumnName("hashed_password")
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Created)
                .HasColumnName("created")
                .IsRequired();

            entity.HasIndex(p => p.Username);
        });
    }
}
