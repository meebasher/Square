using Microsoft.EntityFrameworkCore;
using Square.Domain.Entities;

namespace Square.Infra.Data.Context
{
    public class SquareDbContext : DbContext
    {
        public DbSet<Point> Points { get; set; }

        public SquareDbContext(DbContextOptions<SquareDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point>()
                .HasIndex(p => new { p.X, p.Y })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
