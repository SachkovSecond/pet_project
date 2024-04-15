using Microsoft.EntityFrameworkCore;
using PetProject.Models;

namespace PetProject.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Post> posts => Set<Post>();
        public ApplicationDbContext() => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(nameof(ApplicationDbContext)));
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}