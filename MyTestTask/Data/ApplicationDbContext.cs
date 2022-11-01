using MyTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace MyTestTask.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ad = modelBuilder.Entity<Ad>();
            ad.ToTable("DataBaseAd");
            ad.HasKey(b=>b.Id);
            ad.Property(b => b.Number).HasMaxLength(30);


            var person = modelBuilder.Entity<Person>();
            person.ToTable("DataBasePerson");
            person.HasKey(b => b.Id);
            person.HasMany(p => p.Advertising).WithOne(p => p.Persons);
            person.Property(b => b.Name).HasMaxLength(20);
        }
        public DbSet<Person>? Persons { get; set; }
        public DbSet<Ad>? Ad { get; set; }
    }
}
