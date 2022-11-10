using CategoryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Category.Infrastructure
{
    public sealed class CategoryApiDbContext : DbContext
    {

        public CategoryApiDbContext(DbContextOptions<CategoryApiDbContext> options) : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CategoryApiDbContext).Assembly);
        }

    }
}
