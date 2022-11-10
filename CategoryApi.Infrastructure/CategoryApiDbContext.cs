using CategoryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CategoryApi.Infrastructure;
public sealed class CategoryApiDbContext : DbContext
{

    public CategoryApiDbContext(DbContextOptions<CategoryApiDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
            throw new ArgumentNullException(nameof(modelBuilder));

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryApiDbContext).Assembly);
    }

}
