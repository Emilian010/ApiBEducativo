using Microsoft.EntityFrameworkCore;

namespace Api.DataAgent
{
    public class ApplicationDbContext : DbContext
    {
        //public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("DefaultConnection"); // Or your chosen provider
        }
    }
}
