using MyTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace MyTestTask.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Person>? Persons { get; set; }
        public DbSet<Advertising>? Advertisings { get; set; }
        public DbSet<Subcategories>? Subcategories { get; set; }
        public DbSet<Categories>? Categories { get; set; }
        public DbSet<Cities>? Cities { get; set; }
    }
}
